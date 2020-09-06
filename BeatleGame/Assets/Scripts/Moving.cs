using UnityEngine;

public class Moving : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _moveVector;

    private Rigidbody2D _rigidBody;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 moveDirection = _moveVector.position - transform.position;
        _rigidBody.MovePosition(_rigidBody.position + moveDirection * _speed * Time.fixedDeltaTime);
    }
}
