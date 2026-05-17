using System.Linq;
using UnityEngine;
using YG;

public class SkinManager : MonoBehaviour
{
    [SerializeField] SkinsData _skinsData;

    private void Start()
    {
        var skin = _skinsData.skins.Where(x => x.Id == YG2.saves.SkinId).First();
        Instantiate(skin.Skin, GameManager.Instance.PlayerTransform); 
    }
}
