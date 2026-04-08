using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] int _currentLineId;

    LineManager _lineManager;
    GameObject _player;

    public void Initialize(LineManager lineManager, int currentLineId, GameObject player)
    {
        _currentLineId = currentLineId;
        _lineManager = lineManager;
        _player = player;
        transform
            .SetPositionAndRotation(
            new(player.transform.position.x - 2, player.transform.position.y + 1, _lineManager.Lines[_currentLineId]), 
            player.transform.rotation);
    }

    private void Update()
    {
        if (_player.transform.position.x > transform.position.x - 2)
            transform.position += _speed * 2 * Time.deltaTime * transform.forward;

        transform.position += _speed * Time.deltaTime * transform.forward;
    }
}
