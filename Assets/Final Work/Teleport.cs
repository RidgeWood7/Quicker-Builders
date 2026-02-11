using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System.Linq;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Vector2[] _spawnPoints;

    private List<Collision2D> _enteredCollision = new();

    [SerializeField] private UnityEvent _onAllPlayersJoin;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _enteredCollision.Add(collision);

        if (_enteredCollision.Count == FindObjectsByType<PlayerInput>(FindObjectsSortMode.None).Where(item => !item.GetComponent<Player_Movement>().isDead_D).Count())
        {
            _onAllPlayersJoin.Invoke();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _enteredCollision.Remove(collision);
    }

    public void TP()
    {
        foreach (var player in FindObjectsByType<PlayerInput>(FindObjectsSortMode.None))
        {
            player.transform.position = _spawnPoints[player.playerIndex];
            player.GetComponent<Player_Movement>().isDead_D = false;
            player.GetComponent<Animator>().SetBool("Dying", false);
        }
    }
}
