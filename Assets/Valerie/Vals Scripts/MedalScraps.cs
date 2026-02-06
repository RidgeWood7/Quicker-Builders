using System;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(ParticleSystem))]
public class ParticleGravityTrigger : MonoBehaviour
{
    public enum HeightSpace { World, Local }

    [Header("Activation")]
    [Tooltip("Height at which an individual particle will begin to feel gravity (interpreted in the chosen HeightSpace).")]
    [SerializeField] private float activationHeight = 1.0f;

    [Tooltip("Whether the activation height is interpreted in world or local particle-system space.")]
    [SerializeField] private HeightSpace heightSpace = HeightSpace.World;

    [Header("Gravity")]
    [Tooltip("If true, once a particle crosses the activation height it uses the Particle System's gravity settings (recommended).")]
    [SerializeField] private bool useSystemGravity = true;

    [Tooltip("When not using the system gravity, this downward acceleration (units/s²) will be applied to crossed particles.")]
    [SerializeField] private float customGravity = 9.81f;

    [Tooltip("If true, gravity behavior is enabled.")]
    [SerializeField] private bool enabledGravity = true;

    [Header("Debug")]
    [Tooltip("Draw a gizmo line at the activation height in the Scene view.")]
    [SerializeField] private bool drawGizmo = true;

    private ParticleSystem ps;
    private ParticleSystem.Particle[] particles = new ParticleSystem.Particle[1024];
    private readonly HashSet<uint> seededParticles = new HashSet<uint>();

    // Stored original system settings so we can disable system gravity and restore later
    private ParticleSystem.MinMaxCurve originalGravityModifier;
    private bool originalExternalForcesEnabled;
    private bool originalGravityStored = false;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        StoreAndDisableSystemGravity();
    }

    private void OnEnable()
    {
        // When component enabled at runtime ensure system gravity is disabled.
        StoreAndDisableSystemGravity();
    }

    private void OnDisable()
    {
        RestoreSystemGravity();
    }

    private void OnDestroy()
    {
        RestoreSystemGravity();
    }

    private void StoreAndDisableSystemGravity()
    {
        if (ps == null) ps = GetComponent<ParticleSystem>();
        if (ps == null) return;

        var main = ps.main;
        // store original gravity modifier once
        if (!originalGravityStored)
        {
            originalGravityModifier = main.gravityModifier;
            originalGravityStored = true;
        }

        // disable system gravity by setting gravityModifier to 0
        main.gravityModifier = new ParticleSystem.MinMaxCurve(0f);

        // external forces module can also impart gravity-like forces; disable it and store state
        var ext = ps.externalForces;
        originalExternalForcesEnabled = ext.enabled;
        ext.enabled = false;
    }

    private void RestoreSystemGravity()
    {
        if (ps == null) ps = GetComponent<ParticleSystem>();
        if (ps == null || !originalGravityStored) return;

        var main = ps.main;
        main.gravityModifier = originalGravityModifier;

        var ext = ps.externalForces;
        ext.enabled = originalExternalForcesEnabled;
    }

    private void Update()
    {
        if (!enabledGravity || ps == null) return;

        var main = ps.main;
        bool simWorld = main.simulationSpace == ParticleSystemSimulationSpace.World;

        int max = main.maxParticles;
        if (particles.Length < max) particles = new ParticleSystem.Particle[max];

        int count = ps.GetParticles(particles);
        if (count == 0) return;

        var presentSeeds = new HashSet<uint>();

        for (int i = 0; i < count; i++)
        {
            ref var p = ref particles[i];

            // Compute particle Y in the same space as the chosen activationHeight
            float particleY;
            if (heightSpace == HeightSpace.World)
            {
                particleY = simWorld ? p.position.y : transform.TransformPoint(p.position).y;
            }
            else
            {
                particleY = simWorld ? transform.InverseTransformPoint(p.position).y : p.position.y;
            }

            uint seed = p.randomSeed;
            presentSeeds.Add(seed);

            bool hasCrossed = seededParticles.Contains(seed);

            if (!hasCrossed && particleY >= activationHeight)
            {
                seededParticles.Add(seed);
                hasCrossed = true;
            }

            if (hasCrossed)
            {
                // Compute effective gravity to apply for this particle.
                Vector3 effectiveGravityWorld;

                if (useSystemGravity && originalGravityStored)
                {
                    // Evaluate original gravity modifier for this particle's age if curve is used.
                    float gravityModifierValue = 1f;
                    try
                    {
                        switch (originalGravityModifier.mode)
                        {
                            case ParticleSystemCurveMode.Constant:
                                gravityModifierValue = originalGravityModifier.constant;
                                break;

                            case ParticleSystemCurveMode.Curve:
                                if (originalGravityModifier.curve != null)
                                {
                                    float normAge = ComputeNormalizedAge(p);
                                    gravityModifierValue = originalGravityModifier.curve.Evaluate(normAge) * originalGravityModifier.curveMultiplier;
                                }
                                break;

                            case ParticleSystemCurveMode.TwoConstants:
                                // fallback to average of the two constants if present
                                gravityModifierValue = originalGravityModifier.constant; // MinMaxCurve.constant returns reasonable default
                                break;

                            case ParticleSystemCurveMode.TwoCurves:
                                // fallback: sample curve if curveMax exists, otherwise use multiplier
                                if (originalGravityModifier.curveMax != null)
                                {
                                    float normAge = ComputeNormalizedAge(p);
                                    gravityModifierValue = originalGravityModifier.curveMax.Evaluate(normAge) * originalGravityModifier.curveMultiplier;
                                }
                                break;
                        }
                    }
                    catch (Exception)
                    {
                        gravityModifierValue = originalGravityModifier.constant;
                    }

                    effectiveGravityWorld = Physics.gravity * gravityModifierValue;
                }
                else
                {
                    effectiveGravityWorld = Vector3.down * customGravity;
                }

                // Convert gravity into particle velocity space (local velocities require local gravity vector)
                Vector3 effectiveGravityInParticleSpace = simWorld ? effectiveGravityWorld : transform.InverseTransformDirection(effectiveGravityWorld);

                // Apply gravity to particle velocity
                Vector3 vel = p.velocity;
                vel += effectiveGravityInParticleSpace * Time.deltaTime;
                p.velocity = vel;
            }
        }

        // Remove seeds that belong to dead particles
        seededParticles.IntersectWith(presentSeeds);

        ps.SetParticles(particles, count);
    }

    private float ComputeNormalizedAge(ParticleSystem.Particle p)
    {
        // normalized age in [0,1], where 0 = just born, 1 = end of life
        float start = p.startLifetime;
        float remaining = p.remainingLifetime;
        if (start <= 0f) return 0f;
        float age = Mathf.Clamp01(1f - (remaining / start));
        return age;
    }

    private void OnDrawGizmosSelected()
    {
        if (!drawGizmo) return;

        var p = GetComponent<ParticleSystem>();
        if (p == null) return;

        bool simWorld = p.main.simulationSpace == ParticleSystemSimulationSpace.World;

        Vector3 start, end;
        if (heightSpace == HeightSpace.World)
        {
            start = new Vector3(transform.position.x - 5f, activationHeight, transform.position.z - 5f);
            end = new Vector3(transform.position.x + 5f, activationHeight, transform.position.z + 5f);
        }
        else
        {
            start = transform.TransformPoint(new Vector3(-5f, activationHeight, -5f));
            end = transform.TransformPoint(new Vector3(5f, activationHeight, 5f));
        }

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(start, end);
    }
}