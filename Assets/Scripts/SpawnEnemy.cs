using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public int EnemyCount { get; private set; }

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
        if (_prefabEnemy == null) Debug.LogError("Префаб врага не найден");

        _playerTransform = GameManager.Instance.PlayerTransform;
        _playerMove = GameManager.Instance.PlayerMove;
        _linesCount = GameManager.Instance.LineManager.Lines.Count;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _spawnTime && EnemyCount < _maxEnemies)  // _maxEnemies -> _maxEnemy
        {
            var rnd = Random.Range(0, _linesCount);
            var lineCoord = -3 + rnd * 3;

            Instantiate(_prefabEnemy)
                .GetComponent<Enemy>()
                .Initialize(lineCoord);

            EnemyCount++;
            _timer = 0f;
        }
    }

    public void OneEnemyPutOfBounds()
    {
        EnemyCount--;
    }
}
