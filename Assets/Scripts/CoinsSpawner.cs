using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinsSpawner: MonoBehaviour
{
    const float _spawnTime = 1f;

    [SerializeField] private int _numberOfCoinsInLevel;
    //[SerializeField] TextMeshProUGUI _text;
    [SerializeField] LineManager _lineManager;
    [SerializeField] GameObject _prefabCoin;
    [SerializeField] Transform _playerTransform;
    [SerializeField] GameManager _gameManager;

    float _timer;
    private int _wallsCount = 0;


    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _spawnTime)
        {
            var lineId = Random.Range(0, _lineManager.Lines.Count);

            Instantiate(_prefabCoin)
                .GetComponent<Coin>()
                .Initialize(_gameManager, _lineManager, lineId);
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
