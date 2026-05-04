using System.Collections.Generic;
using UnityEngine;

public class WallsSpawner : MonoBehaviour
{
    [SerializeField] LineManager _lineManager;
    [SerializeField] GameObject _prefabWall;
    [SerializeField] Transform _playerTransform;
    [SerializeField] float _spawnTime;

    float _timer;
    private int _wallsCount = 0;
    private Queue<Wall> _walls;


    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _spawnTime)
        {
            var lineId = Random.Range(0, _lineManager.Lines.Count);

            var newWall = Instantiate(_prefabWall)
                .GetComponent<Wall>();

            newWall.Initialize(_lineManager, _playerTransform, lineId);

            _timer = 0f;
        }
    }
}
