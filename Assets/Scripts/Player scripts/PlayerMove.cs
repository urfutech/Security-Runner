using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    const float GravityForce = 67;
    const float SpeedForwardChange = 0.01f;
    const float JumpForce = 22;
    const float SpeedChangeLine = 25;

    public int CurrentLineId { get; private set; }

    [SerializeField] float _speedForward;
    [SerializeField] float _maxSpeed;
    [SerializeField] LineManager _lineManager;
    [SerializeField] GameObject _road1;
    [SerializeField] GameObject _road2;

    private bool _isChangeLine;
    private CharacterController _charController;
    private float _verticalSpeed;

    void Initialize()  // Нужно ли это? - На всякий пожарный
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
        CheckRoads();
    }

    private void CheckRoads()
    {
        if (transform.position.x >= _road1.transform.position.x + 190)
            _road1.transform.position = new Vector3(_road2.transform.position.x + 200, 0, 0);
        if (transform.position.x >= _road2.transform.position.x + 190)
            _road2.transform.position = new Vector3(_road1.transform.position.x + 200, 0, 0);
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
        _speedForward += SpeedForwardChange;
        if (_speedForward > _maxSpeed) _speedForward = _maxSpeed;
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

    bool CheckGround() => Physics.Raycast(transform.position, Vector3.down, 0.05f);

    public float GetSpeed() => _speedForward;
}
