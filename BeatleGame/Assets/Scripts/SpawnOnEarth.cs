using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SpawnOnEarth : MonoBehaviour
{
    public static SpawnOnEarth instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private GameObject _spikeSpawnPrefab;
    [SerializeField] private GameObject _ghostSpawnPrefab;

    [SerializeField] private Transform _ghostSpawnPoint;

    [SerializeField] private float _earthRadius;

    private int _score;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _scoreTextLosePanel;

    [SerializeField] private GameObject _losePanel;

    private void Start()
    {
        ChangeScore(0);
    }

    public void SpawnEnemies()
    {
        SpikeRandomSpawn();
        GhostRandomSpawn();
    }

    private void SpikeRandomSpawn()
    {
        Vector2 coordinates = GetSpawnCoordinates();
        //Vector2 opositeCoordinates = new Vector2(-coordinates.x, coordinates.y);

        Instantiate(_spikeSpawnPrefab, coordinates, Quaternion.identity);
        //Instantiate(_spikeSpawnPrefab, opositeCoordinates, Quaternion.identity);
    }

    private void GhostRandomSpawn()
    {
        Instantiate(_ghostSpawnPrefab, _ghostSpawnPoint.position, Quaternion.identity);
    }

    private Vector2 GetSpawnCoordinates()
    {
        // coordinate fromula for circle: X^2 + Y^2 = R^2
        float xSpawnCoordinate = Random.Range(-_earthRadius, -1);
        float ySpawnCoordinate = -Mathf.Sqrt(Mathf.Pow(_earthRadius, 2) - Mathf.Pow(xSpawnCoordinate, 2));

        return new Vector2(xSpawnCoordinate, ySpawnCoordinate);
    }

    public void ChangeScore(int scoreAmount)
    {
        _score = scoreAmount;
        _scoreText.text = "Score: " + _score.ToString();
    }

    public void IncreaseScore(int scoreAmount)
    {
        _score += scoreAmount;
        _scoreText.text = "Score: " + _score.ToString();
    }

    public void GameOver()
    {
        Time.timeScale = 0f;

        if (_score > PlayerPrefs.GetInt("Score", 0))
            PlayerPrefs.SetInt("Score", _score);

        _scoreTextLosePanel.text = PlayerPrefs.GetInt("Score").ToString();

        _losePanel.SetActive(true);
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
