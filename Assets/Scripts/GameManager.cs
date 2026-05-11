using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;

    public Transform PlayerTransform;
    public PlayerMove PlayerMove;
    public GameObject CanvasLose;
    public LineManager LineManager;
    public SpawnEnemy EnemySpawner;
    public WallsSpawner WallsSpawner;
    public CoinsSpawner CoinsSpawner;


    void Start()
    {
        if (PlayerTransform == null) Debug.LogError("PlayerTransform не назначен");
        if (PlayerMove == null) Debug.LogError("PlayerMove не назначен");
        if (CanvasLose == null) Debug.LogError("CanvasLose не назначен");
        if (LineManager == null) Debug.LogError("LineManager не назначен");
        if (EnemySpawner == null) Debug.LogError("EnemySpawner не назначен");
        if (WallsSpawner == null) Debug.LogError("WallsSpawner не назначен");
        if (CoinsSpawner == null) Debug.LogError("CoinsSpawner не назначен");

        ScoreText.text = "Счёт: 0";
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
}
