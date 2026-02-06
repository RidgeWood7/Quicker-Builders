using UnityEngine;

public class Coin : MonoBehaviour
{
    private bool _isAttatched = false;
    private bool _playerDead = false;
    private Transform _targetTransform;
    [SerializeField] private Transform _coin;
    [SerializeField] private float FollowDistance;

    void Update()
    {
        if (_playerDead == true && _isAttatched == true)
        {
            _isAttatched = false;
            _targetTransform = null;
        }

        if (_isAttatched == true && Vector2.Distance(transform.position,_targetTransform.position)>FollowDistance)
        {
            _coin.position = Vector2.Lerp(_coin.position, _targetTransform.position, Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.gameObject.CompareTag("Player") && _playerDead == false && _isAttatched == false)
        {
            _isAttatched = true;
            _targetTransform = collision.gameObject.transform;
        }
    }
}
