using System;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private Transform _playerTransform;
    private Queue<Transform> _walls;

    public void Initialize(Queue<Transform> walls)
    {
        _playerTransform = GameManager.Instance.PlayerTransform;
        _walls = GameManager.Instance.WallsSpawner.Walls;
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
