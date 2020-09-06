using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float G;
    
    private Transform _planet;

    [SerializeField] private float _planetMass;

    private Rigidbody2D _rigidBody;

    private float _playerMass;
    private float _distance;
    private float _forceValue;

    private float _lookAngle;
    private Vector3 _lookDirection;

    private void Start()
    {
        _planet = GameObject.FindWithTag("Planet").transform;

        _rigidBody = GetComponent<Rigidbody2D>();
        _playerMass = _rigidBody.mass;
        _distance = Vector2.Distance(_planet.position, transform.position);
        _forceValue = (G * _planetMass * _playerMass) / Mathf.Pow(_distance, 2);
    }

    private void Update()
    {
        _lookDirection = _planet.position - transform.position;
        _lookAngle = Mathf.Atan2(_lookDirection.y, _lookDirection.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, _lookAngle);
    }

    private void FixedUpdate()
    {
        Vector2 forceDirection = (_planet.position - transform.position).normalized;
        _rigidBody.AddForce(_forceValue * forceDirection);
    }
}
