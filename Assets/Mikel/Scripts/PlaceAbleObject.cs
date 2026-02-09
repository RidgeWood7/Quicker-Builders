using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceAbleObject : MonoBehaviour
{
    public bool isGrabbed;
    public PlayerInput player;

    void Start()
    {
        isGrabbed = true;
    }

    private void Update()
    {
        if (isGrabbed)
        {
            Vector2 screenPos = player.GetComponent<CursorLink>().cursor.transform.position;
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            worldPos.x = Mathf.Round(worldPos.x);
            worldPos.y = Mathf.Round(worldPos.y);

            transform.position = worldPos;
        }
    }

    public void Place(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            Debug.Log("Place");
            isGrabbed = false;

            player.GetComponent<CursorLink>().cursor.gameObject.SetActive(false);
            player.actionEvents[2].RemoveListener(Place);
        }
    }
}
