using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    const float speedChangeLine = 3;

    LineManager _lineManager;
    Transform _playerTransform;
    // bool _needBoost;
    float _speed;
    PlayerMove _playerMove;

    private Ray forwardRay;
    private Ray centerRay;
    private Ray rightRay;//пока нахуй не нужно
    private Ray leftRay; //левый и правый
    private CharacterController charControl;
    private SpawnEnemy enemySpawner;
    private bool isChangingLine;
    private int currentLineCoord;
    //разные если меняет направление
    private int toLineCoord;
    private float timer;
    private float pupaTime;

    public void Initialize(int lineCoord)
    {
        InitRay(centerRay, 0);
        InitRay(rightRay, -3);
        InitRay(leftRay, 3);
        this.currentLineCoord = lineCoord;
        toLineCoord = currentLineCoord;
        _lineManager = GameManager.Instance.LineManager;
        _playerTransform = GameManager.Instance.
            PlayerTransform;
        _speed = GameManager.Instance.PlayerMove.Speed;
        _speed *= 1.2f;
        if (_speed > 
            GameManager.Instance.PlayerMove.MaxSpeed)
            _speed = GameManager.Instance
                .PlayerMove.MaxSpeed;
        _playerMove = GameManager.Instance.PlayerMove;
        transform.SetPositionAndRotation(
            new Vector3(
                _playerTransform.position.x + 2,
                0,
                lineCoord),
            _playerTransform.rotation);
        enemySpawner = GameManager.Instance.EnemySpawner;

        isChangingLine = false;

        forwardRay = new Ray(transform.position,
            transform.forward);
        charControl = GetComponent<CharacterController>();
    }

    private void InitRay(Ray ray, int zCoord)
    {
        ray = new Ray(new Vector3(
              transform.position.x,
              transform.position.y,
              zCoord
              ),
            transform.forward);
    }

    private void Update()
    {
        MoveForward();
        CheckAndMoveDirection();
        RandomSwitchDirectionIfPossible();
    }

    private void RandomSwitchDirectionIfPossible()
    {
        if (!isChangingLine)
        {
            if(currentLineCoord == 0) 
            {
                var rnd = Random.Range(0, 2);
                if (rnd == 0 
                    && Physics.Raycast(rightRay,
                    out RaycastHit hit))
                {
                    if (transform.position.x + 2
                        < hit.transform.position.x)
                    {
                        isChangingLine = true;
                        toLineCoord = 3;
                        return;
                    }
                }
                else
                {
                    if (Physics.Raycast(leftRay,
                          out RaycastHit hit1))
                    {
                        if (transform.position.x + 2 
                            < hit1.transform.position.x)
                        {
                            isChangingLine = true;
                            toLineCoord = -3;
                            return;
                        }
                    }
                }
            }
            else
            {
                if (Physics.Raycast(centerRay,
                      out RaycastHit hit2))
                {
                    if (transform.position.x + 2 
                        < hit2.transform.position.x)
                    {
                        isChangingLine = true;
                        toLineCoord = 0;
                        return;
                    }
                }
            }
        }
    }
 
    private void MoveForward()
    {
        if (transform.position.x + 5
            < _playerTransform.position.x
            || Math.Abs(transform.position.y) > 5
            || Math.Abs(transform.position.z) > 5)
        {
            enemySpawner.OneEnemyPutOfBounds();
            Die();
        }

        if (transform.position.x <
            _playerTransform.position.x + 1.5
            && _speed < _playerMove.Speed)
        {
            _speed = _playerMove.Speed;
        }

        var forwardMove = transform.forward
            * _speed * Time.deltaTime;
        charControl.Move(forwardMove);
    }

    private void CheckAndMoveDirection()
    {
        if (Physics.Raycast(forwardRay, out RaycastHit hit)
            && !isChangingLine)
        {
            if (hit.collider.gameObject.CompareTag("Wall"))
            {
                if (hit.transform.position.x <
                    transform.position.x + 5)
                {
                    GetDirection();
                    isChangingLine = true;
                }
            }
        }

        if (isChangingLine) ChangeDirectionALittle();
    }

    private void ChangeDirectionALittle()
    {
        var newDirection = 
            transform.right 
            * (currentLineCoord - toLineCoord)
            * speedChangeLine
            * Time.deltaTime;
        charControl.Move(newDirection);

        forwardRay = new Ray(transform.position,
            transform.forward);
        if (Math.Abs(Math.Abs(transform.position.z)
            - Math.Abs(toLineCoord)) < 0.1f)
        {
            currentLineCoord = toLineCoord;
            isChangingLine = false;
        }
    }

    private void GetDirection()
    {
        if (currentLineCoord == -3
            || currentLineCoord == 3)
        {
            toLineCoord = 0;
        }
        else
        {
            var rnd = Random.Range(0, 2);
            if (rnd == 0)
                toLineCoord = -3;
            else toLineCoord = 3;
        }
    }


    public void Die()
    {
        Destroy(gameObject);
    }

}
