using UnityEngine;

public class Cursor : MonoBehaviour
{
    public Player_Movement player;

    private void OnEnable()
    {
        player.enabled = false;
        player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
    }

    private void OnDisable()
    {
        player.enabled = true;
    }
}
