using UnityEngine;

public class Cursor : MonoBehaviour
{
    public Player_Movement player;

    private void OnEnable()
    {
        if (player)
        {
            player.enabled = false;
            player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }
    }

    private void OnDisable()
    {
        if (player)
        {
            player.enabled = true;
        }
    }
}
