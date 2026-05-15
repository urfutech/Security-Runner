using UnityEngine;

public class RoadUpdater : MonoBehaviour
{
    [SerializeField] GameObject _road1;
    [SerializeField] GameObject _road2;

    Transform _playerTransform;
    float _offsetX;

    void Start()
    {
        if (_road1 == null) Debug.LogError("Road1 не назначен");
        if (_road2 == null) Debug.LogError("Road2 не назначен");

        _playerTransform = GameManager.Instance.PlayerTransform;
        _offsetX = _road1.GetComponent<Renderer>().bounds.size.x;
    }

    void Update()
    {
        if (_playerTransform.position.x >= _road1.transform.position.x + 5)
            _road1.transform.position = new Vector3(_road2.transform.position.x + _offsetX, 0, 0);
        if (_playerTransform.position.x >= _road2.transform.position.x + 5)
            _road2.transform.position = new Vector3(_road1.transform.position.x + _offsetX, 0, 0);
    }
}
