using UnityEngine;
using System.Collections;

public class StartBeatle : MonoBehaviour
{
    [SerializeField] private Sprite _disableSprite;
    [SerializeField] private Sprite _enableSprite;

    private SpriteRenderer _spriteRenderer;

    private Collider2D _collider;

    private int _counter;

    private void Start()
    {
        _counter = 0;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();

        StartCoroutine(Enable());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Destroy(enemy);
            }

            StartCoroutine(TouchedByPlayer());
            _counter++;

            if (_counter == 3)
            {
                collision.gameObject.GetComponent<PlayerController>().Heal(1);
                _counter = 0;
            }
        }
    }

    private IEnumerator TouchedByPlayer()
    {
        SpawnOnEarth.instance.IncreaseScore(5);
        SpawnOnEarth.instance.SpawnEnemies();

        _spriteRenderer.sprite = _enableSprite;
        yield return new WaitForSeconds(2f);
        _spriteRenderer.sprite = _disableSprite;
    }

    private IEnumerator Enable()
    {
        yield return new WaitForSeconds(2f);
        _collider.enabled = true;
    }
}
