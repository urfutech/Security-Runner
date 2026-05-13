using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCollision : MonoBehaviour
{
    PlayerMove _playerMove;

    private void Start()
    {
        _playerMove = GameManager.Instance.PlayerMove;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
            hit.gameObject.GetComponent<Enemy>().Die();
        }
        else if (hit.gameObject.CompareTag("Side Wall"))
        {
            _playerMove.ChangeSpeed(-_playerMove.GetSpeed / 3);
        }
        else if (hit.gameObject.CompareTag("Wall"))
        {
            GameManager.Instance.GameLose();
        }
    }
}
