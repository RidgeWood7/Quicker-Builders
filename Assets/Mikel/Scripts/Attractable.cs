using UnityEngine;

public class Attractable : MonoBehaviour
{
    [SerializeField] private bool rotateToCenter = true;
    [SerializeField] private Attractor currentAttractor;
    [SerializeField] private float gravityStrength = 100f;

    Transform m_transform;
    Collider2D m_collider;
    Rigidbody2D m_rigidbody;

    private void Start()
    {
        m_transform = GetComponent<Transform>();
        m_collider = GetComponent<Collider2D>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (currentAttractor != null)
        {
            if (!currentAttractor.AttractedObjects.Contains(m_collider))
            {
                currentAttractor = null;
                return;
            }
            if (rotateToCenter) RotateToCenter();
            m_rigidbody.gravityScale = 0;
        }
        else
        {
            m_rigidbody.gravityScale = 1;
        }
    }

    public void Attract(Attractor attractorObj)
    {
        Vector2 attractionDir = ((Vector2)attractorObj.attractorTransform.position - m_rigidbody.position).normalized;
        m_rigidbody.AddForce(attractionDir * -attractorObj.gravity * gravityStrength * Time.fixedDeltaTime);

        if(currentAttractor == null)
        {
            currentAttractor = attractorObj;
        }
    }

    void RotateToCenter()
    {
        if (currentAttractor != null)
        {
            Vector2 distanceVector = (Vector2)currentAttractor.attractorTransform.position - (Vector2)m_transform.position;
            float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
            m_transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        }
    }

}
