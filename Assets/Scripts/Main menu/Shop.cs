using System.Linq;
using TMPro;
using UnityEngine;
using YG;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject _menu;
    [SerializeField] SkinsData _skinsData;
    [SerializeField] TextMeshProUGUI _textCoins;
    [SerializeField] GameObject _frame;
    [SerializeField] GameObject[] _buttons;

    private void Start()
    {
        _frame.transform.SetParent(_buttons[YG2.saves.SkinId].transform);
        _frame.transform.SetAsFirstSibling();
        _frame.transform.localPosition = Vector3.zero;

        _textCoins.text = $"Coins: {YG2.saves.Coins}";
    }

    public void BackToMenu()
    {
        _menu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void SetSkin(int skinId, GameObject button)
    {
        if (skinId == YG2.saves.SkinId) return;

        var skin = _skinsData.skins.Where(x => x.Id == skinId).FirstOrDefault();

        if (skin == null)
        {
            Debug.LogWarning($"Скин с ID {skinId} не найден");
            return;
        }

        if (skin.IsUnlocked)
        {
            YG2.saves.SkinId = skinId;

            _frame.transform.SetParent(button.transform);
            _frame.transform.SetAsFirstSibling();
            _frame.transform.localPosition = Vector3.zero;
        }
        else
        {
            BuySkin(skin, button);
        }

        YG2.SaveProgress();
    }

    public void BuySkin(SkinInfo skin, GameObject button)
    {
        if (YG2.saves.Coins >= skin.Cost)
        {
            YG2.saves.Coins -= skin.Cost;
            YG2.saves.SkinId = skin.Id;
            YG2.saves.IdUnlockedSkins.Add(skin.Id);
            skin.IsUnlocked = true;
            _textCoins.text = $"Coins: {YG2.saves.Coins}";

            _frame.transform.SetParent(button.transform);
            _frame.transform.SetAsFirstSibling();
            _frame.transform.localPosition = Vector3.zero;
        }
    }
}
