using System;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private GameManager _gameManager;
    private Transform _playerTransform;
    private Queue<Transform> _walls;

    public void Initialize(GameManager _gameManager,
        Queue<Transform> walls, int lineId)
    {
        _playerTransform = _gameManager.PlayerTransform;
        _walls = _gameManager.WallsSpawner.Walls;
    }

    private void Update()
    {
        if (_playerTransform.position.x - 5 > transform.position.x)
        {
            Destroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }

    public event Action<Wall> Destroyed;
}
