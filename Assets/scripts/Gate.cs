using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private int _value;
    [SerializeField] private DeformationType _deformationType;
    [SerializeField] private GateAppearance _gateAppearance;

    private void OnValidate()
    {
        _gateAppearance.UpdateVisual(_deformationType, _value);
    }
}
