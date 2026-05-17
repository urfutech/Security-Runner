using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WallsSpawner : MonoBehaviour
{
    const float ImportantTimerCoef = 0.1f;

    public Queue<Transform> Walls;

    [SerializeField] GameObject _prefabWall;
    [SerializeField] float _spawnTime;

    float _timer;
    int _linesCount;
    Transform _playerTransform;
    PlayerMove _playerMove;
    LineManager _lineManager;
    private float randomDensity = 1;    
    private float wallsRow;
    private float wallsPlaced;

    private void Start()
    {
        if (_prefabWall == null) Debug.LogError("Префаб стены не найден");

        _playerMove = GameManager.Instance.PlayerMove;
        _lineManager = GameManager.Instance.LineManager;
        _playerTransform = GameManager.Instance.PlayerTransform;
        _linesCount = GameManager.Instance.LineManager.Lines.Count;
        Debug.Log("Why?");
        Walls = new Queue<Transform>();
        Debug.Log(Walls.Count);
    }


    private void Update()
    {
        _timer += Time.deltaTime;
        var timer = _timer 
            * _playerMove.Speed
            * ImportantTimerCoef;

        var randomPlacemnetCoef = Random.Range(
            0.8f, 1.2f);
            
        if (timer > _spawnTime 
            * randomDensity * randomPlacemnetCoef)
        {
            var lineId = Random.Range(0, _linesCount);
            var newWall = Instantiate(_prefabWall).GetComponent<Wall>();

            newWall.Initialize(Walls);
            newWall.transform.position = new(
                _playerTransform.position.x + 50,
                2,
                _lineManager.Lines[lineId]);

            Walls.Enqueue(newWall
                .GetComponent<Transform>());
            newWall.Destroyed += DestroyWall;
            wallsPlaced++;
            if (wallsPlaced == wallsRow)
            {
                wallsRow = Random.Range(5, 30);
                randomDensity = Random.Range(
                    0.4f, 1f);
                wallsPlaced = 0;
            }

            _timer = 0f;

        }
    }

    private void DestroyWall(Wall wall)
    {
        wall.Destroyed -= DestroyWall;
    }
}
