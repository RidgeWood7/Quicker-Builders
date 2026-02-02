using UnityEngine;

public class Trap : MonoBehaviour
{
    private Player_Movement _playerMovement;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerMovement = collision.gameObject.GetComponent<Player_Movement>;
        }
    }
}
