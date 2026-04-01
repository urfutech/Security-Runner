using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float _oldMousePositionX;
    private float _eulerY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _oldMousePositionX = Input.mousePosition.x;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 newPosition = transform.position + transform.forward * _speed * Time.deltaTime;
            newPosition.x = Mathf.Clamp(newPosition.x, -2.5f, 2.5f);
            transform.position = newPosition;

            float deltaX = Input.mousePosition.x - _oldMousePositionX;
            _oldMousePositionX = Input.mousePosition.x;

            _eulerY += deltaX;
            _eulerY = Mathf.Clamp(_eulerY, -70, 70);
            transform.eulerAngles = new Vector3(0, _eulerY, 0);
        }


    }
}
