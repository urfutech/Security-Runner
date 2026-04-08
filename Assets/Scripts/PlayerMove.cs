using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public int CurrentLineId { get; private set; }

    [SerializeField] float _speedForward;
    [SerializeField] float _speedChangeLine;
    [SerializeField] LineManager _lineManager;
    [SerializeField] GameObject _road1;
    [SerializeField] GameObject _road2;

    bool _isChangeLine;

    void Initialize()  // Нужно ли это?
    {  
        _road2.transform.position = new Vector3(_road1.transform.position.x + 200, 0, 0);
    }

    private void Start()
    {
        CurrentLineId = _lineManager.DefaultLineId;
    }


    void Update()
    {
        Movement();  // Движение персонажа

        if (transform.position.x >= _road1.transform.position.x + 190)
            _road1.transform.position = new Vector3(_road2.transform.position.x + 200, 0, 0);
        if (transform.position.x >= _road2.transform.position.x + 190)
            _road2.transform.position = new Vector3(_road1.transform.position.x + 200, 0, 0);
    }

    void Movement()
    {
        transform.position += _speedForward * Time.deltaTime * transform.forward;

        if (Input.GetKeyDown(KeyCode.D) && CurrentLineId > 0)
        {
            CurrentLineId--;
            _isChangeLine = true;
        }
        if (Input.GetKeyDown(KeyCode.A) && CurrentLineId < _lineManager.Lines.Count - 1)
        {
            CurrentLineId++;
            _isChangeLine = true;
        }

        if (_isChangeLine)
        {
            var target = new Vector3(transform.position.x, transform.position.y, _lineManager.Lines[CurrentLineId]);
            transform.position = Vector3.MoveTowards(transform.position, target, _speedChangeLine * Time.deltaTime);

            if (transform.position == target)
                _isChangeLine = true;
        }
    }
}