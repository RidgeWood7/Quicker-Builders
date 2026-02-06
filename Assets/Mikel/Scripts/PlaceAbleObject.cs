using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceAbleObject : MonoBehaviour
{
    public bool isGrabbed;
    public Vector2 position;
    public bool isMouseControlled;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrabbed&& isMouseControlled)
        {
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            mousePosition.z = 10f; // Set this to be the distance from the camera to the object
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            worldPosition.x=Mathf.Round(worldPosition.x);
            worldPosition.y=Mathf.Round(worldPosition.y);

            transform.position = worldPosition;
        }
    }

    public void PlacedDown(InputAction.CallbackContext ctx)
    {
         isGrabbed = true;
    }

    public void Move()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = 10f; // Set this to be the distance from the camera to the object
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPosition.x = Mathf.Round(worldPosition.x);
        worldPosition.y = Mathf.Round(worldPosition.y);

        transform.position = worldPosition;
    }
}
