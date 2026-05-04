using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;

    [SerializeField] public Transform PlayerTransform;
    [SerializeField] public PlayerMove PlayerMove;
    [SerializeField] public GameObject CanvasLose;
    [SerializeField] public LineManager LineManager;
    [SerializeField] public GameObject EdlessField;
    [SerializeField] public SpawnEnemy EnemySpawner;
    [SerializeField] public GameObject PrefabEnemy;
    [SerializeField] public WallsSpawner WallsSpawner;
    [SerializeField] public GameObject PrefabWall;
    [SerializeField] public CoinsSpawner CoinsSpawner;


    void Start()
    {
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
