using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] LineManager _lineManager;
    [SerializeField] int _currentLineId;

    void Update()
    {
        transform.position += _speed * Time.deltaTime * transform.forward;

        if (Input.GetKeyDown(KeyCode.D) && _currentLineId > 0)
            transform.position = new(transform.position.x, transform.position.y, _lineManager.Lines[--_currentLineId]);

        if (Input.GetKeyDown(KeyCode.A) && _currentLineId < _lineManager.Lines.Count)
            transform.position = new(transform.position.x, transform.position.y, _lineManager.Lines[++_currentLineId]);
    }
}