using UnityEngine;

public class Coin : MonoBehaviour
{
    const float _rotationMultiPlayer = 15;

    Transform _playerTransform;
    PlayerMove _playerMove;
    CoinsSpawner _coinsSpawner;

    public void Initialize(GameManager gameManager, int lineId)
    {
        var playerTransform = gameManager.PlayerTransform;
        _playerTransform = GameManager.Instance.PlayerTransform;
        _playerMove = GameManager.Instance.PlayerMove;
        _coinsSpawner = GameManager.Instance.CoinsSpawner;

        transform.position = new(playerTransform.position.x + 50, 1.2f, gameManager.LineManager.Lines[lineId]);
    }

    void Update()
    {
        var rotationSpeed = _rotationMultiPlayer 
            * Time.deltaTime
            * _playerMove.GetSpeed;

        transform.Rotate(0, rotationSpeed, 0);

        if (_playerTransform.position.x - 5 > transform.position.x)
        {
            _coinsSpawner.DestroyedOne();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter()
    {
        FindAnyObjectByType<CoinsSpawner>().AddOne();
        Destroy(gameObject);
    }
}
