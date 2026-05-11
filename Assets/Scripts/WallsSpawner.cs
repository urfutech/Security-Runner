using UnityEngine;
using Random = UnityEngine.Random;

public class WallsSpawner : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    [SerializeField] GameObject _prefabWall;
    [SerializeField] float _spawnTime;

    float _timer;
    int _wallsCount;
    int _linesCount;
    Transform _playerTransform;

    private void Start()
    {
        if (_gameManager == null) Debug.LogError("GameManager не назначен");
        if (_prefabWall == null) Debug.LogError("Префаб стены не найден");

        _playerTransform = _gameManager.PlayerTransform;
        _linesCount = _gameManager.LineManager.Lines.Count;
    }


    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _spawnTime)
        {
            var lineId = Random.Range(0, _linesCount);
            var newWall = Instantiate(_prefabWall).GetComponent<Wall>();

            newWall.Initialize(_gameManager.LineManager, _playerTransform, lineId);
            newWall.Destroyed += DestroyWall;
            _wallsCount++;

            _timer = 0f;
        }
    }

    private void DestroyWall(Wall wall)
    {
        wall.Destroyed -= DestroyWall;
        _wallsCount--;
    }
}
