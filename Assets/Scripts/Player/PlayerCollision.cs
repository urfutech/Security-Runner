using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCollision : MonoBehaviour
{
    PlayerMove _playerMove;
    Progress _progress;

    private void Start()
    {
        _playerMove = GameManager.Instance.PlayerMove;
        _progress = Progress.Instance;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
            hit.gameObject.GetComponent<Enemy>().Die();
        }
        else if (hit.gameObject.CompareTag("Side Wall"))
        {
            _playerMove.ChangeSpeed(-_playerMove.Speed / 3);
        }
        else if (hit.gameObject.CompareTag("Wall"))
        {
            GameManager.Instance.GameLose();
        }
        else if (hit.gameObject.CompareTag("Coin"))  // При падении на монету, вызывается много раз
        {
            hit.gameObject.GetComponent<Coin>().Die();
            _progress.AddCoin();
        }
    }
}
