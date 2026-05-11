using UnityEngine;

public class Enemy : MonoBehaviour
{
    int _currentLineId;
    LineManager _lineManager;
    Transform _playerTransform;
    // bool _needBoost;
    float _speed;
    PlayerMove _playerMove;

    public void Initialize
        (GameManager gameManager, int currentLineId, float mult)
    {
        _currentLineId = currentLineId;
        _lineManager = gameManager.LineManager;
        _playerTransform = gameManager.PlayerTransform;
        _speed = gameManager.PlayerMove.GetSpeed;
        // _needBoost = true;
        _playerMove = gameManager.PlayerMove;
        transform.SetPositionAndRotation(
            new(_playerTransform.position.x + 2, 0, _lineManager.Lines[_currentLineId]),
            _playerTransform.rotation);
    }

    private void Update()
    {
        // if (_needBoost)
        // {
        //     if (_playerTransform.position.x > transform.position.x - 3)
        //         transform.position += _speed * 2 * Time.deltaTime * transform.forward;
        //     else
        //         _needBoost = false;
        // }
        // else
        // {
        //     transform.position += _speed * Time.deltaTime * transform.forward;
        // }

        if (_playerTransform.position.x > transform.position.x - 3)
            _speed = _playerMove.GetSpeed;

        transform.position += _speed * Time.deltaTime * transform.forward;

        if (_playerTransform.position.x - 5 > transform.position.x)
            Destroy(gameObject);
    }
}
