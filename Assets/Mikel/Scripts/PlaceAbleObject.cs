using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceAbleObject : MonoBehaviour
{
    public bool isGrabbed;
    public int playerIndex;

    private void Update()
    {
        Vector2 screenPos = FindObjectsByType<PlayerInput>(FindObjectsInactive.Include, FindObjectsSortMode.None).First(item => item.playerIndex == playerIndex).GetComponent<CursorLink>().cursor.transform.position;
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        transform.position = worldPos;
    }
}
