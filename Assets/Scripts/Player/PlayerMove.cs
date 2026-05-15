using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    const float GravityForce = 67;
    const float SpeedForwardChange = 0.01f;
    const float JumpForce = 22;
    const float SpeedChangeLine = 25;
    const float StartSpeed = 10;

    public int CurrentLineId { get; private set; }

    [SerializeField] float _speedForward;
    [SerializeField] float _maxSpeed;

    bool _isChangeLine;
    CharacterController _charController;
    float _verticalSpeed;
    LineManager _lineManager;

    private void Start()
    {
        _lineManager = GameManager.Instance.LineManager;
        _speedForward = _speedForward < StartSpeed ? StartSpeed : _speedForward;
        CurrentLineId = _lineManager.DefaultLineId;
        _charController = GetComponent<CharacterController>();
    }


    void Update()
    {
        Jump();
        MoveCharacter();
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && (_charController.isGrounded || CheckGround()))
            _verticalSpeed = JumpForce;

        if (!_charController.isGrounded)
            _verticalSpeed -= GravityForce * Time.deltaTime;
        else if (_verticalSpeed < 0)
            _verticalSpeed = 0;
    }

    void MoveCharacter()
    {
        _speedForward += SpeedForwardChange * Time.deltaTime;

        if (_speedForward > _maxSpeed) 
            _speedForward = _maxSpeed;

        var nextPos = _speedForward * Time.deltaTime * transform.forward;
        nextPos.y = _verticalSpeed * Time.deltaTime;

        _charController.Move(nextPos);
        CheckDirection();
    }

    private void CheckDirection()
    {
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
        if (_isChangeLine) MoveDirection();
    }

    private void MoveDirection()
    {
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
                var moveStep = SpeedChangeLine * Time.deltaTime * directionToTarget.normalized;

                if (moveStep.magnitude > directionToTarget.magnitude)
                    moveStep = directionToTarget;

                _charController.Move(moveStep);
            }
        }
    }

    public void ChangeSpeed(float speed) =>
        _speedForward = _speedForward + speed < StartSpeed ? StartSpeed : _speedForward + speed; 

    bool CheckGround() => Physics.Raycast(transform.position, Vector3.down, 0.05f);

    public float Speed => _speedForward;

    public float MaxSpeed => _maxSpeed;

    public float SpawnTiming => 
        _speedForward * 0.1f;
}
