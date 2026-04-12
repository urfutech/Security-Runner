using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int _currentLineId;

    LineManager _lineManager;
    Transform _playerTransform;
    bool _needBoost;
    float _speed;

    public void Initialize
        (LineManager lineManager, int currentLineId, Transform playerTransform, PlayerMove playerMove, float mult)
    {
        _currentLineId = currentLineId;
        _lineManager = lineManager;
        _playerTransform = playerTransform;
        _speed = playerMove.GetSpeed() * mult;
        _needBoost = true;
        transform.SetPositionAndRotation(
            new(_playerTransform.position.x - 2, 1, _lineManager.Lines[_currentLineId]),
            _playerTransform.rotation);
    }

    private void Update()
    {
        if (_needBoost)
        {
            if (_playerTransform.position.x > transform.position.x - 2)
                transform.position += _speed * 2 * Time.deltaTime * transform.forward;
            else
                _needBoost = false;
        }
        else
        {
            transform.position += _speed * Time.deltaTime * transform.forward;
        }

        if (_playerTransform.position.x - 5 > transform.position.x)
            Destroy(gameObject);
    }
}
