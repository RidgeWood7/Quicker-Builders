using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Finish : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Time.timeScale = 0f;
    }
}
