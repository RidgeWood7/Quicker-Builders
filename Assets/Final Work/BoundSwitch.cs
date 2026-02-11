using Unity.Cinemachine;
using UnityEngine;

public class BoundSwitch : MonoBehaviour
{
    [SerializeField] private BoxCollider2D collider1;
    [SerializeField] private BoxCollider2D collider2;
    private CinemachineConfiner2D confiner;

    private void Start()
    {
        confiner = GetComponent<CinemachineConfiner2D>();
    }

    public void SwitchCamBoundsTo1()
    {
        if (confiner != null)
        {
            confiner.BoundingShape2D = collider1;
        }
    }
    public void SwitchCamBoundsTo2()
    {
        if (confiner != null)
        {
            confiner.BoundingShape2D = collider2;
        }
    }
}
