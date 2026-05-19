using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI")]
    [SerializeField] TextMeshProUGUI _textCountCoins;
    [SerializeField] TextMeshProUGUI _scoreTextLoseMenu;
    [SerializeField] TextMeshProUGUI _scoreTextCurrent;
    [SerializeField] TextMeshProUGUI _scoreTextBest;
    [SerializeField] GameObject _canvasLose;
    [SerializeField] RawImage _iconUser;
    [SerializeField] GameObject _review;

    [Header("Player")]
    [SerializeField] Transform _playerTransform;
    [SerializeField] PlayerMove _playerMove;

    [Header("Managers")]
    [SerializeField] LineManager _lineManager;
    [SerializeField] SkinManager _skinManager;

    [Header("Spawners")]
    [SerializeField] SpawnEnemy _enemySpawner;
    [SerializeField] WallsSpawner _wallsSpawner;
    [SerializeField] CoinsSpawner _coinsSpawner;
    [SerializeField] BoostsSpawner _boostsSpawner;

    [Header("Unity editor")]
    [SerializeField] GameObject _createBoost;

	#region Properties
	public TextMeshProUGUI TextCountCoins => _textCountCoins;
    public TextMeshProUGUI ScoreTextLoseMenu => _scoreTextLoseMenu;
    public TextMeshProUGUI ScoreTextCurrent => _scoreTextCurrent;
    public TextMeshProUGUI ScoreTextBest => _scoreTextBest;
    public GameObject CanvasLose => _canvasLose;
    public GameObject Review => _review;
    public RawImage IconUser => _iconUser;
    public Transform PlayerTransform => _playerTransform;
    public PlayerMove PlayerMove => _playerMove;
    public LineManager LineManager => _lineManager;
    public SkinManager SkinManager => _skinManager;
    public SpawnEnemy EnemySpawner => _enemySpawner;
    public WallsSpawner WallsSpawner => _wallsSpawner;
    public CoinsSpawner CoinsSpawner => _coinsSpawner;
    public BoostsSpawner BoostsSpawner => _boostsSpawner;
	#endregion

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
    }

    private void Start()
    {
        if (ScoreTextCurrent != null) ScoreTextCurrent.text = "Score: 0";
        if (ScoreTextBest != null) ScoreTextBest.text = $"Best score: {YG2.saves.BestScore}";
        if (TextCountCoins != null) TextCountCoins.text = YG2.saves.Coins.ToString();
        if (Review != null && !YG2.reviewCanShow) Review.SetActive(false);

        #if !UNITY_EDITOR
        _createBoost.SetActive(false);
        #endif
    }

    public void GameLose()
    {
        Progress.Instance.NewBestScore();

        CoinsSpawner.enabled = false;
        EnemySpawner.enabled = false;
        WallsSpawner.enabled = false;
        PlayerMove.enabled = false;

        _scoreTextLoseMenu.text = _scoreTextCurrent.text;
        CanvasLose.SetActive(true);
    }

    private void CheckNullReferences()
    {
        if (PlayerTransform == null) Debug.LogError("PlayerTransform не назначен");
        if (TextCountCoins == null) Debug.LogError("TextCountCoins не назначен");
        if (ScoreTextLoseMenu == null) Debug.LogError("ScoreTextLoseMenu не назначен");
        if (ScoreTextCurrent == null) Debug.LogError("ScoreTextCurrent не назначен");
        if (ScoreTextBest == null) Debug.LogError("ScoreTextBest не назначен");
        if (PlayerMove == null) Debug.LogError("PlayerMove не назначен");
        if (CanvasLose == null) Debug.LogError("CanvasLose не назначен");
        if (LineManager == null) Debug.LogError("LineManager не назначен");
        if (EnemySpawner == null) Debug.LogError("EnemySpawner не назначен");
        if (WallsSpawner == null) Debug.LogError("WallsSpawner не назначен");
        if (CoinsSpawner == null) Debug.LogError("CoinsSpawner не назначен");
        if (Review == null) Debug.LogError("Review не назначен");
        if (BoostsSpawner == null) Debug.LogError("BoostsSpawner не назначен");
    }
}
