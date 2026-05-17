using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

[RequireComponent(typeof(Camera))]
public class Shop : MonoBehaviour
{
    [SerializeField] GameObject _menu;
    [SerializeField] SkinsData _skinsData;
    [SerializeField] TextMeshProUGUI _textCoins;
    [SerializeField] Button[] _buttons;
    [SerializeField] Vector2 _spriteSize = new(60, 60);

    ColorBlock _colorBlock;
    ColorBlock _defaultColorBlock;
    ColorBlock _lockedColorBlock;
    Camera _renderCamera;

    void Awake()
    {
        _renderCamera = GetComponent<Camera>();
        _renderCamera.enabled = false;
    }

    private void Start()
    {
        _defaultColorBlock = _buttons[0].colors;
        _lockedColorBlock = _buttons[0].colors;
        _colorBlock = _buttons[0].colors;

        _lockedColorBlock.normalColor = new(180f / 255f, 180f / 255f, 180f / 255f);
        _lockedColorBlock.selectedColor = new(180f / 255f, 180f / 255f, 180f / 255f);
        _lockedColorBlock.pressedColor = new(180f / 255f, 180f / 255f, 180f / 255f);
        _colorBlock.normalColor = new(160f / 255f, 229f / 255f, 44f / 255f);

        _buttons[YG2.saves.SkinId].colors = _colorBlock;

        _textCoins.text = YG2.saves.Coins.ToString();

        for (int i = 0; i < _buttons.Length; i++)
        {
            var button = _buttons[i];
            var skinData = _skinsData.skins[i];

            button.transform.Find("Cost").GetComponentInChildren<TextMeshProUGUI>().text = skinData.Cost.ToString();
            button.transform.Find("Preview").GetComponentInChildren<Image>().sprite = CreateSpriteFromPrefab(skinData.Skin);

            if (!skinData.IsUnlocked)
                button.colors = _lockedColorBlock;
        }
    }

    public void BackToMenu()
    {
        YG2.InterstitialAdvShow();
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
        else
        {

        }
    }

    public Sprite CreateSpriteFromPrefab(GameObject prefab)
    {
        var renderContainer = new GameObject("TempRenderContainer");
        var tempLayer = LayerMask.NameToLayer("TempRender");
        var tempObj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        var cameraObj = new GameObject("TempRenderCamera");
        var tempCamera = cameraObj.AddComponent<Camera>();
        var renderTexture = new RenderTexture((int)_spriteSize.x, (int)_spriteSize.y, 24, RenderTextureFormat.ARGB32);
        var texture = new Texture2D((int)_spriteSize.x, (int)_spriteSize.y, TextureFormat.RGBA32, false);

        renderContainer.transform.SetParent(null);
        tempObj.transform.SetParent(renderContainer.transform);
        SetLayerRecursively(tempObj, tempLayer);

        tempCamera.CopyFrom(_renderCamera);
        tempCamera.clearFlags = CameraClearFlags.SolidColor;
        tempCamera.backgroundColor = Color.clear;
        tempCamera.cullingMask = 1 << tempLayer;
        tempCamera.transform.position = tempObj.transform.position + new Vector3(0, 1.5f, 2f);
        tempCamera.transform.LookAt(tempObj.transform.position + Vector3.up * 1.25f);
        tempCamera.targetTexture = renderTexture;
        tempCamera.Render();

        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0, 0, _spriteSize.x, _spriteSize.y), 0, 0);
        texture.Apply();

        var sprite = Sprite.Create(texture, new Rect(0, 0, _spriteSize.x, _spriteSize.y), Vector2.one * 0.5f);

        tempCamera.targetTexture = null;
        RenderTexture.active = null;
        DestroyImmediate(renderTexture);
        DestroyImmediate(cameraObj);
        DestroyImmediate(renderContainer);

        return sprite;
    }

    private void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }
}