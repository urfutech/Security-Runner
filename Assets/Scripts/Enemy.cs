using UnityEngine;

public class Enemy : MonoBehaviour
{
    int _currentLineId;
    LineManager _lineManager;
    Transform _playerTransform;
    // bool _needBoost;
    float _speed;
    PlayerMove _playerMove;

    private bool isChangeLine;
    private Ray ForwardRay;
    private Ray RightRay;//пока нахуй не нужно
    private Ray LeftRay; //левый и правый

    public void Initialize(int currentLineId, float mult)
    {
        _currentLineId = currentLineId;
        _lineManager = GameManager.Instance.LineManager;
        _playerTransform = GameManager.Instance.PlayerTransform;
        _speed = GameManager.Instance.PlayerMove.Speed;
        // _needBoost = true;
        _playerMove = GameManager.Instance.PlayerMove;
        transform.SetPositionAndRotation(
            new(_playerTransform.position.x + 2, 0, _lineManager.Lines[_currentLineId]),
            _playerTransform.rotation);
        ForwardRay = new Ray(transform.position, transform.forward);
        isChangeLine = false;
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
        
        if (Physics.Raycast(ForwardRay, out RaycastHit hit))
        {
            if (hit.transform.position.x <
                transform.position.x + 2)
                if (hit.collider.gameObject.CompareTag("Wall"))
                    ChangeDirection();
        }

        if (_playerTransform.position.x > transform.position.x - 3)
            _speed = _playerMove.Speed;

        transform.position += _speed * Time.deltaTime * transform.forward;

        if (_playerTransform.position.x - 5 > transform.position.x)
            Destroy(gameObject);
    }

    private void ChangeDirection()
    {
        if (_currentLineId < 0)
            _currentLineId = 0;
        else if (_currentLineId > 0)
            _currentLineId = 0;
        else if (_currentLineId == 0)
        {
            var rnd = Random.Range(0, 10);
            if (rnd < 5)
                _currentLineId = -3;
            else
                _currentLineId = 3;
        }

        isChangeLine = true;
        ForwardRay = new Ray(transform.position,
            transform.forward);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

}
