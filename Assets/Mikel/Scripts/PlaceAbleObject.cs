using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceAbleObject : MonoBehaviour
{
    public bool isGrabbed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
          isGrabbed = false;
        }
        if (isGrabbed)
        {
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            mousePosition.z = 10f; // Set this to be the distance from the camera to the object
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = worldPosition;
        }
    }
}
