using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinsSpawner: MonoBehaviour
{
    const float _spawnTime = 0.3f;

    // [SerializeField] TextMeshProUGUI _text;
    [SerializeField] GameObject _prefabCoin;
    [SerializeField] GameManager _gameManager;

    int _numberOfCoinsInLevel;
    float _timer;
    int _lineCount;
    private Queue<Transform> _walls;

    private void Start()
    {
        if (_gameManager == null) Debug.LogError("GameManager не назначен");
        if (_prefabCoin == null) Debug.LogError("Префаб монеты не найден");

        _lineCount = _gameManager.LineManager.Lines.Count;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _spawnTime)
        {
            var lineId = Random.Range(0, _lineCount);

            while (_gameManager.WallsSpawner
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
