using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinsSpawner: MonoBehaviour
{
    const float _spawnTime = 0.3f;

    // [SerializeField] TextMeshProUGUI _text;
    [SerializeField] GameObject _prefabCoin;

    int _numberOfCoinsInLevel;
    float _timer;
    int _lineCount;
    WallsSpawner _wallsSpawner;
    GameManager _gameManager;
    private Queue<Transform> _walls;

    private void Start()
    {
        if (_prefabCoin == null) Debug.LogError("Префаб монеты не найден");

        _gameManager = GameManager.Instance;
        _wallsSpawner = _gameManager.WallsSpawner;
        _lineCount = _gameManager.LineManager.Lines.Count;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _spawnTime)
        {
            var lineId = Random.Range(0, _lineCount);

            while (_wallsSpawner
                .Walls.Peek().position.x == lineId)
            {
                lineId = Random.Range(0, _lineCount);
            }

            Instantiate(_prefabCoin)
                .GetComponent<Coin>()
                .Initialize(_gameManager, lineId);
            AddOne();

            _timer = 0f;
        }
    }

    public void DestroyedOne()
    {
        _numberOfCoinsInLevel--;
    }

    public void AddOne()
    {
        _numberOfCoinsInLevel++;
        //_text.text = _numberOfCoinsInLevel.ToString();
    }
}
