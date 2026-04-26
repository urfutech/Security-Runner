using UnityEngine;

public class WallsSpawner : MonoBehaviour
{
    public int WallsCount { get; private set; }

    [SerializeField] LineManager _lineManager;
    [SerializeField] GameObject _prefabWall;
    [SerializeField] Transform _playerTransform;
    [SerializeField] int _maxWalls;
    [SerializeField] float _spawnTime;

    float _timer;
    private int _wallsCount = 0;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _spawnTime && _wallsCount < _maxWalls)
        {
            var lineId = Random.Range(0, _lineManager.Lines.Count);

            Instantiate(_prefabWall)
                .GetComponent<Wall>()
                .Initialize(_lineManager, _playerTransform, lineId);

            _wallsCount++;
            _timer = 0f;
        }
    }
}
