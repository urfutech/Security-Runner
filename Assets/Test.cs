using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] float _height;
    [SerializeField] int _numberOfCoins;
    [SerializeField] string _name;
    [SerializeField] Color _bodyColor;
    [SerializeField] Vector3 _startPosition;
    [SerializeField] bool _isAlive;

    [SerializeField] Light _sun;
    [SerializeField] Camera _camera;
    [SerializeField] Transform _ball;

    void Start()
    {
        transform.localScale = new Vector3(1, _height, 1);
        gameObject.name = _name;
        gameObject.GetComponent<Renderer>().material.color = _bodyColor;
        transform.position = _startPosition;
        gameObject.SetActive(true);

        _sun.intensity = 2;
        _sun.color = _bodyColor;
        _camera.fieldOfView = 120f;
    }

    void Update()
    {
        _ball.position = transform.position + new Vector3(0f, 3f, 0f);
    }

}