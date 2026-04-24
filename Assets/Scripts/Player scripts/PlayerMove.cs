using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    public int CurrentLineId { get; private set; }

    [SerializeField] float _speedForward;
    const float _speedForwardChange = 1;
    [SerializeField] float _speedChangeLine;
    [SerializeField] float _gravityForce;
    [SerializeField] float _jumpForce;
    [SerializeField] LineManager _lineManager;
    [SerializeField] GameObject _road1;
    [SerializeField] GameObject _road2;

    bool _isChangeLine;
    CharacterController _charController;
    float _verticalSpeed;

    void Initialize()  // Нужно ли это?
    {  
        _road2.transform.position = new Vector3(_road1.transform.position.x + 200, 0, 0);
    }

    private void Start()
    {
        CurrentLineId = _lineManager.DefaultLineId;
        _charController = GetComponent<CharacterController>();
    }


    void Update()
    {
        Jump();
        MoveCharacter();

        if (transform.position.x >= _road1.transform.position.x + 190)
            _road1.transform.position = new Vector3(_road2.transform.position.x + 200, 0, 0);
        if (transform.position.x >= _road2.transform.position.x + 190)
            _road2.transform.position = new Vector3(_road1.transform.position.x + 200, 0, 0);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && (_charController.isGrounded || CheckGround()))
            _verticalSpeed = _jumpForce;

        if (!_charController.isGrounded)
            _verticalSpeed -= _gravityForce * Time.deltaTime;
        else if (_verticalSpeed < 0)
            _verticalSpeed = 0;
    }

    void MoveCharacter()
    {
        _speedForward += _speedForwardChange;
        var nextPos = _speedForward * Time.deltaTime * transform.forward;
        nextPos.y = _verticalSpeed * Time.deltaTime;
        _charController.Move(nextPos);

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
            var targetPosition = new Vector3(transform.position.x, transform.position.y, _lineManager.Lines[CurrentLineId]);
            var directionToTarget = targetPosition - transform.position;

            if (directionToTarget.magnitude < 0.01f)
            {
                transform.position = targetPosition;
                _isChangeLine = false;
            }
            else
            {
                var moveStep = _speedChangeLine * Time.deltaTime * directionToTarget.normalized;

                if (moveStep.magnitude > directionToTarget.magnitude)
                    moveStep = directionToTarget;

                _charController.Move(moveStep);
            }
        }
    }

    bool CheckGround() => Physics.Raycast(transform.position, Vector3.down, 0.05f);

    public float GetSpeed() => _speedForward;
}