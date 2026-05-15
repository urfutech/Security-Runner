using System.Linq;
using UnityEngine;
using YG;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject _menu;
    [SerializeField] SkinsData _skinsData;

    public void BackToMenu()
    {
        _menu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void SetSkin(int skinId)
    {
        var skin = _skinsData.skins.Where(x => x.Id == skinId).FirstOrDefault();

        if (skin == null)
        {
            Debug.LogWarning($"Скин с ID {skinId} не найден");
            return;
        }

        if (skin.IsUnlocked)
            YG2.saves.SkinId = skinId;
        else
            BuySkin(skin);

        YG2.SaveProgress();
    }

    public void BuySkin(SkinInfo skin)
    {
        if (YG2.saves.Coins >= skin.Cost)
        {
            YG2.saves.Coins -= skin.Cost;
            YG2.saves.SkinId = skin.Id;
            YG2.saves.IdUnlockedSkins.Add(skin.Id);
            skin.IsUnlocked = true;
        }
    }
}
