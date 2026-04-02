using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public int CurrentLineId;

    [SerializeField] float _speed;
    [SerializeField] LineManager _lineManager;

    void Update()
    {
        transform.position += _speed * Time.deltaTime * transform.forward;

        if (Input.GetKeyDown(KeyCode.D) && CurrentLineId > 0)
            transform.position = new(transform.position.x, transform.position.y, _lineManager.Lines[--CurrentLineId]);

        if (Input.GetKeyDown(KeyCode.A) && CurrentLineId < _lineManager.Lines.Count - 1)
            transform.position = new(transform.position.x, transform.position.y, _lineManager.Lines[++CurrentLineId]);
    }
}