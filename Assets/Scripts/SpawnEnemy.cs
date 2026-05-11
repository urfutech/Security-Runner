using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public int EnemyCount { get; private set; }

    [SerializeField] GameManager _gameManager;

    [Header("Настройка врага")]
    [SerializeField] GameObject _prefabEnemy;
    [SerializeField] int _maxEnemy;
    [SerializeField] float _spawnTime;
    // Во сколько раз больше будет изначальная скорость по сравнению с игроком
    [SerializeField] float _multiplierSpeed;

    Transform _playerTransform;
    PlayerMove _playerMove;
    float _timer;
    int _maxEnemies = 1;  // TEMP
    int _linesCount;

    private void Start()
    {
        if (_gameManager == null) Debug.LogError("GameManager не назначен");
        if (_prefabEnemy == null) Debug.LogError("Префаб врага не найден");

        _playerTransform = _gameManager.PlayerTransform;
        _playerMove = _gameManager.PlayerMove;
        _linesCount = _gameManager.LineManager.Lines.Count;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _spawnTime && EnemyCount < _maxEnemies)  // _maxEnemies -> _maxEnemy
        {
            var lineId = Random.Range(0, _linesCount);
            if (lineId == _playerMove.CurrentLineId)
                lineId = (lineId + 1) % _linesCount;

            Instantiate(_prefabEnemy)
                .GetComponent<Enemy>()
                .Initialize(_gameManager, lineId, _multiplierSpeed);

            EnemyCount++;
            _timer = 0f;
        }
    }
}
