using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCollision : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
            Destroy(hit.gameObject);
        }
        else if (hit.gameObject.CompareTag("Wall"))
        {
            _gameManager.GameLose();
        }
    }
}
