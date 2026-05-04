using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public int EnemyCount { get; private set; }

    [SerializeField] GameManager _gameManager;
    [SerializeField] LineManager _lineManager;
    [SerializeField] GameObject _prefabEnemy;
    [SerializeField] int _maxEnemy;
    [SerializeField] float _spawnTime;
    // Во сколько раз больше будет изначальная скорость по сравнению с игроком
    [SerializeField] float _multiplierSpeed;

    float _timer;
    [SerializeField] Transform _playerTransform;
    [SerializeField] PlayerMove _playerMove;

    // private void Initialize()
    // {
    // }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _spawnTime && EnemyCount < _maxEnemy)
        {
            var lineId = Random.Range(0, _lineManager.Lines.Count);
            if (lineId == _playerMove.CurrentLineId)
                lineId = (lineId + 1) % _lineManager.Lines.Count;

            Instantiate(_prefabEnemy)
                .GetComponent<Enemy>()
                .Initialize(_lineManager, lineId, _playerTransform, _playerMove, _multiplierSpeed);

            EnemyCount++;
            _timer = 0f;
        }
    }
}
