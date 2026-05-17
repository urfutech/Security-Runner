using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject _menu;
    [SerializeField] SkinsData _skinsData;
    [SerializeField] TextMeshProUGUI _textCoins;
    [SerializeField] Button[] _buttons;

    ColorBlock _colorBlock;
    ColorBlock _defaultColorBlock;

    private void Start()
    {
        _defaultColorBlock = _buttons[0].colors;
        _colorBlock = _buttons[0].colors;
        _colorBlock.normalColor = new(160f / 255f, 229f / 255f, 44f / 255f);

        _buttons[YG2.saves.SkinId].colors = _colorBlock;

        _textCoins.text = YG2.saves.Coins.ToString();
    }

    public void BackToMenu()
    {
        _menu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void SetSkin(int skinId, Button button)
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
            _buttons[YG2.saves.SkinId].colors = _defaultColorBlock;

            YG2.saves.SkinId = skinId;

            button.colors = _colorBlock;
        }
        else
        {
            BuySkin(skin, button);
        }

        YG2.SaveProgress();
    }

    public void BuySkin(SkinInfo skin, Button button)
    {
        if (YG2.saves.Coins >= skin.Cost)
        {
            _buttons[YG2.saves.SkinId].colors = _defaultColorBlock;
            button.colors = _colorBlock;

            YG2.saves.Coins -= skin.Cost;
            YG2.saves.SkinId = skin.Id;
            YG2.saves.IdUnlockedSkins.Add(skin.Id);
            skin.IsUnlocked = true;
            _textCoins.text = YG2.saves.Coins.ToString();
        }
    }
}
