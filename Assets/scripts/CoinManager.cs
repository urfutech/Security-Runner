using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private int _numberOfCoinsInLevel;
    [SerializeField] TextMeshProUGUI _text;

    public void AddOne()
    {
        _numberOfCoinsInLevel++;
        _text.text = _numberOfCoinsInLevel.ToString();
    }
}
