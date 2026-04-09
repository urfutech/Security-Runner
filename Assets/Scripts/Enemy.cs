using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int _currentLineId;

    LineManager _lineManager;
    GameObject _player;
    bool _needBoost;
    float _speed;

    public void Initialize(LineManager lineManager, int currentLineId, GameObject player, float mult)
    {
        _currentLineId = currentLineId;
        _lineManager = lineManager;
        _player = player;
        _speed = _player.GetComponent<PlayerMove>().GetSpeed() * mult;
        _needBoost = true;
        transform
            .SetPositionAndRotation(
            new(player.transform.position.x - 2, player.transform.position.y + 1, _lineManager.Lines[_currentLineId]), 
            player.transform.rotation);
    }

    private void Update()
    {
        if (_needBoost)
        {
            if (_player.transform.position.x > transform.position.x - 2)
                transform.position += _speed * 2 * Time.deltaTime * transform.forward;
            else
                _needBoost = false;
        }
        else
        {
            transform.position += _speed * Time.deltaTime * transform.forward;
        }
    }
}
