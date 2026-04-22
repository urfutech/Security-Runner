using UnityEngine;

public class Wall : MonoBehaviour
{
    Transform _playerTransform;

    public void Initialize(LineManager lineManager, Transform playerTransform, int lineId)
    {
        _playerTransform = playerTransform;

        transform.position = new(_playerTransform.position.x + 50, 2, lineManager.Lines[lineId]);
    }

    private void Update()
    {
        if (_playerTransform.position.x - 5 > transform.position.x)
            Destroy(gameObject);
    }
}
