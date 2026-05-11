using UnityEngine;

public class Coin : MonoBehaviour
{
    const float _rotationMultiPlayer = 15;

    GameManager _gameManager;
    Transform _playerTransform;

    public void Initialize(GameManager gameManager, int lineId)
    {
        var playerTransform = gameManager.PlayerTransform;
        _gameManager = gameManager;
        _playerTransform = _gameManager.PlayerTransform;

        transform.position = new(playerTransform.position.x + 50, 1.2f, gameManager.LineManager.Lines[lineId]);
    }

    void Update()
    {
        var rotationSpeed = _rotationMultiPlayer 
            * Time.deltaTime
            * _gameManager.PlayerMove.GetSpeed;

        transform.Rotate(0, rotationSpeed, 0);

        if (_playerTransform.position.x - 5 > transform.position.x)
        {
            _gameManager.CoinsSpawner.DestroyedOne();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter()
    {
        FindAnyObjectByType<CoinsSpawner>().AddOne();
        Destroy(gameObject);
    }
}
