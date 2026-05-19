using UnityEngine;

public class BoostsSpawner : MonoBehaviour
{
    [SerializeField] float _minSpawnTime = 60f;
    [SerializeField] float _maxSpawnTime = 180f;

    [SerializeField] GameObject[] _boosterPrefabs;

    float _timer;
    float _currentSpawnTime;
    int _lineCount;

    private void Start()
    {
        if (_boosterPrefabs == null) Debug.LogError("Префабы усилителей не найден");

        _lineCount = GameManager.Instance.LineManager.Lines.Count;
        _currentSpawnTime = Random.Range(_minSpawnTime, _maxSpawnTime);
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _currentSpawnTime)
        {
            CreateBoost();
        }
    }

    public void CreateBoost()
    {
        var lineId = Random.Range(0, _lineCount);
        var randomIndex = Random.Range(0, _boosterPrefabs.Length);

        Instantiate(_boosterPrefabs[randomIndex])
            .GetComponent<Boost>()
            .Initialize(lineId);

        _timer = 0f;
        _currentSpawnTime = Random.Range(_minSpawnTime, _maxSpawnTime);
    }
}
