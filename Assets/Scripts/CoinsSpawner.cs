using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsSpawner: MonoBehaviour
{
    const float _spawnTime = 0.3f;

    [SerializeField] GameObject _prefabCoin;

    float _timer;
    int _lineCount;
    WallsSpawner _wallsSpawner;
    private Queue<Transform> _walls;

    private void Start()
    {
        if (_prefabCoin == null) Debug.LogError("Префаб монеты не найден");

        _wallsSpawner = GameManager.Instance.WallsSpawner;
        _lineCount = GameManager.Instance.LineManager.Lines.Count;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _spawnTime)
        {
            var lineId = Random.Range(0, _lineCount);

            //while (_wallsSpawner.Walls.Peek().position.x == lineId)
            //{
            //    lineId = Random.Range(0, _lineCount);
            //}

            Instantiate(_prefabCoin)
                .GetComponent<Coin>()
                .Initialize(lineId);

            _timer = 0f;
        }
    }
}
