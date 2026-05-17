using UnityEngine;

public class Coin : MonoBehaviour
{
    const float _rotationMultiPlayer = 15;

    Transform _playerTransform;
    PlayerMove _playerMove;

    public void Initialize(int lineId)
    {
        _playerTransform = GameManager.Instance.PlayerTransform;
        _playerMove = GameManager.Instance.PlayerMove;

        transform.position = new(_playerTransform.position.x + 50, 1.2f, GameManager.Instance.LineManager.Lines[lineId]);
    }

    void Update()
    {
        var rotationSpeed = _rotationMultiPlayer 
            * Time.deltaTime
            * _playerMove.Speed;

        transform.Rotate(0, rotationSpeed, 0);

        if (_playerTransform.position.x - 5 > transform.position.x)
        {
            Destroy(gameObject);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        
    }
}
