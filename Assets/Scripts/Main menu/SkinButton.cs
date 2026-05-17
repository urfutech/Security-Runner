using Unity.VisualScripting;
using UnityEngine;

public class SkinButton : MonoBehaviour
{
    [SerializeField] Shop _shop;
    [SerializeField] int _skinId;

    public void OnButtonClick()
    {
        _shop.SetSkin(_skinId, gameObject);
    }
}
