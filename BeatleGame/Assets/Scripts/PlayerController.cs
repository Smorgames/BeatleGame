using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _timeForStop = 3f;
    [SerializeField] private float _jumpForce = 5f;

    private Rigidbody2D _rigidBody;

    private Moving _moving;
    private Gravity _gravity;

    private bool _canJump = true;

    [SerializeField] private Transform _jumpVectorTransform;

    private int _currentHealth;

    [SerializeField] private GameObject[] _healthIcons;
    [SerializeField] private Sprite[] _healthSprites;

    private void Start()
    {
        _canJump = true;
        _currentHealth = 1;

        for (int i = 0; i < _currentHealth; i++)
            _healthIcons[i].GetComponent<Image>().sprite = _healthSprites[1];

        _rigidBody = GetComponent<Rigidbody2D>();
        _moving = GetComponent<Moving>();
        _gravity = GetComponent<Gravity>();
    }

    public void Jump()
    {
        if (_canJump)
        {
            _canJump = false;
            Vector3 jumpVector = _jumpVectorTransform.position - transform.position;
            ComponentController.instance.SetMoving(false);
            _rigidBody.AddForce(jumpVector * _jumpForce, ForceMode2D.Impulse);
        }
    }

    public void Stop()
    {
        StartCoroutine(StopCoroutine());
    }

    private IEnumerator StopCoroutine()
    {
        _rigidBody.velocity = Vector2.zero;
        _gravity.enabled = false;
        _moving.enabled = false;
        _canJump = false;

        yield return new WaitForSeconds(_timeForStop);

        _gravity.enabled = true;
        _moving.enabled = true;
        _canJump = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ComponentController.instance.SetMoving(true);
        _canJump = true;
    }

    public void Damage(int amount)
    {
        _currentHealth -= amount;

        for (int i = 0; i < _currentHealth; i++)
            _healthIcons[i].GetComponent<Image>().sprite = _healthSprites[1];

        for (int i = _currentHealth; i < _healthIcons.Length; i++)
            _healthIcons[i].GetComponent<Image>().sprite = _healthSprites[0];

        if (_currentHealth <= 0)
        {
            Death();
        }
    }

    public void Heal(int amount)
    {
        if (_currentHealth < _healthIcons.Length)
        {
            _currentHealth += amount;
            for (int i = 0; i < _currentHealth; i++)
                _healthIcons[i].GetComponent<Image>().sprite = _healthSprites[1];

            for (int i = _currentHealth; i < _healthIcons.Length; i++)
                _healthIcons[i].GetComponent<Image>().sprite = _healthSprites[0];
        }
    }

    private void Death()
    {
        SpawnOnEarth.instance.GameOver();
    }
}
