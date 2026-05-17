using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SkinButton : MonoBehaviour
{
    [SerializeField] Shop _shop;
    [SerializeField] int _skinId;

    public void OnButtonClick()
    {
        _shop.SetSkin(_skinId, GetComponent<Button>());
    }
}
