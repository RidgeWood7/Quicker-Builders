using UnityEngine;

public class Hazard : MonoBehaviour
{
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player_Movement playerMov = collision.gameObject.GetComponent<Player_Movement>();

        if (playerMov != null)
        {
            playerMov.isDead_D = true;
        }
    }
}

