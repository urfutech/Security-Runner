using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] int _currentLineId;

    LineManager _lineManager;

    public void Initialize(LineManager lineManager, int currentLineId, Transform playerTransform)
    {
        _lineManager = lineManager;
        _currentLineId = currentLineId;
        transform
            .SetPositionAndRotation(
            new(playerTransform.position.x, playerTransform.position.y + 1, _lineManager.Lines[_currentLineId]), 
            playerTransform.rotation);
    }

    private void Update()
    {
        transform.position += _speed * Time.deltaTime * transform.forward;
    }
}
