using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCollision : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;

    private void Start()
    {
        if (_gameManager == null) Debug.LogError("GameManager не назначен");
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
            Destroy(hit.gameObject);
        }
        else if (hit.gameObject.CompareTag("Side Wall"))
        {
            _gameManager.PlayerMove.ChangeSpeed(-_gameManager.PlayerMove.GetSpeed / 3);
        }
        else if (hit.gameObject.CompareTag("Wall"))
        {
            _gameManager.GameLose();
        }
    }
}
