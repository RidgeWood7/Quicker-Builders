using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class TargetTracker : MonoBehaviour
{
    public CinemachineTargetGroup targetGroup;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TrackTarget(PlayerInput target)
    {
        targetGroup.AddMember(target.gameObject.transform, 1, 0);
    }
}
