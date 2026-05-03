using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;

    [SerializeField] Transform _player;
    [SerializeField] SpawnEnemy _spawnEnemy;
    [SerializeField] WallsSpawner _wallsSpawner;
    [SerializeField] PlayerMove _playerMove;
    [SerializeField] GameObject _canvasLose;
    [SerializeField] LineManager _lineManager;
    [SerializeField] GameObject _edlessField;
    [SerializeField] GameObject _prefabEnemy;
    [SerializeField] GameObject _prefabWall;


    void Start()
    {
        ScoreText.text = "Счёт: 0";
    }

    void Update()
    {
        ScoreText.text = $"Счёт: {(int)_player.position.x}";
    }

    public void GameLose()
    {
        _spawnEnemy.enabled = false;
        _wallsSpawner.enabled = false;
        _playerMove.enabled = false;

        _canvasLose.SetActive(true);
    }
}
