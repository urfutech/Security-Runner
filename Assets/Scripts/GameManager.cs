using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI")]
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] GameObject _canvasLose;

    [Header("Player")]
    [SerializeField] Transform _playerTransform;
    [SerializeField] PlayerMove _playerMove;

    [Header("Managers")]
    [SerializeField] LineManager _lineManager;

    [Header("Spawners")]
    [SerializeField] SpawnEnemy _enemySpawner;
    [SerializeField] WallsSpawner _wallsSpawner;
    [SerializeField] CoinsSpawner _coinsSpawner;

    public TextMeshProUGUI ScoreText => _scoreText;
    public GameObject CanvasLose => _canvasLose;
    public Transform PlayerTransform => _playerTransform;
    public PlayerMove PlayerMove => _playerMove;
    public LineManager LineManager => _lineManager;
    public SpawnEnemy EnemySpawner => _enemySpawner;
    public WallsSpawner WallsSpawner => _wallsSpawner;
    public CoinsSpawner CoinsSpawner => _coinsSpawner;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        CheckNullReferences();

        if (ScoreText != null) ScoreText.text = "Счёт: 0";
    }

    void Update()
    {
        ScoreText.text = $"Счёт: {(int)PlayerTransform.position.x}";
    }

    public void GameLose()
    {
        EnemySpawner.enabled = false;
        WallsSpawner.enabled = false;
        PlayerMove.enabled = false;

        CanvasLose.SetActive(true);
    }

    private void CheckNullReferences()
    {
        if (PlayerTransform == null) Debug.LogError("PlayerTransform не назначен");
        if (ScoreText == null) Debug.LogError("ScoreText не назначен");
        if (PlayerMove == null) Debug.LogError("PlayerMove не назначен");
        if (CanvasLose == null) Debug.LogError("CanvasLose не назначен");
        if (LineManager == null) Debug.LogError("LineManager не назначен");
        if (EnemySpawner == null) Debug.LogError("EnemySpawner не назначен");
        if (WallsSpawner == null) Debug.LogError("WallsSpawner не назначен");
        if (CoinsSpawner == null) Debug.LogError("CoinsSpawner не назначен");
    }
}
