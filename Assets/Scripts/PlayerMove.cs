using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _stepWidth;

    void Update()
    {
        transform.position += _speed * Time.deltaTime * transform.forward;

        if (Input.GetKeyDown(KeyCode.A))
            transform.position -= transform.right * _stepWidth;

        if (Input.GetKeyDown(KeyCode.D))
            transform.position += transform.right * _stepWidth;
    }
}