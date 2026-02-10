using UnityEngine;

public class Rotate : MonoBehaviour
{
    private PlaceAbleObject placeAbleObject;

    void Update()
    {
        placeAbleObject = GetComponent<PlaceAbleObject>();

        if (Input.GetKey(KeyCode.R) && placeAbleObject != null && placeAbleObject.isGrabbed == true)
        {
            transform.Rotate(180, 0, 0 * Time.deltaTime);
        }

    }
}
