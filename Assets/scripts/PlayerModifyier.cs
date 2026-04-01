using UnityEngine;

public class PlayerModifyier : MonoBehaviour
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;

    [SerializeField] private Renderer _renderer;

    private float _widthMultiplier = 0.0005f;
    private float _heightMultiplier = 0.01f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            AddWidth(20);
    }

    public void AddWidth(int value)
    {
        _width += value;
        _renderer.material.SetFloat("_PushValue", _width * _widthMultiplier);
    }

    public void AddHeight(int value)
    {
        _height += value;
    }
}
