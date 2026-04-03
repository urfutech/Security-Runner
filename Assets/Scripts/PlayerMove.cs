using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public int CurrentLineId;

    [SerializeField] float _speed;
    [SerializeField] LineManager _lineManager;
    [SerializeField] GameObject _road1;
    [SerializeField] GameObject _road2;

    void Initialize()
    {  
        _road2.transform.position = new Vector3(_road1.transform.position.x + 200, 0, 0);
    }

    void Update()
    {
        transform.position += _speed * Time.deltaTime * transform.forward;

        if (Input.GetKeyDown(KeyCode.D) && CurrentLineId > 0)
            transform.position = new(transform.position.x, transform.position.y, _lineManager.Lines[--CurrentLineId]);

        if (Input.GetKeyDown(KeyCode.A) && CurrentLineId < _lineManager.Lines.Count - 1)
            transform.position = new(transform.position.x, transform.position.y, _lineManager.Lines[++CurrentLineId]);

        if (transform.position.x >= _road1.transform.position.x + 190)
            _road1.transform.position = new Vector3(_road2.transform.position.x + 200, 0, 0);
        if (transform.position.x >= _road2.transform.position.x + 190)
            _road2.transform.position = new Vector3(_road1.transform.position.x + 200, 0, 0);
    }
}