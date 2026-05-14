using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class PlayerInfo
{
    public int BestScore;
    public int Coins;
}

public class Progress : MonoBehaviour
{
    public static Progress Instance { get; private set; }
    public PlayerInfo PlayerInfo;

    [DllImport("__Internal")]
    private static extern void SaveExtern(string data);

    [DllImport("__Internal")]
    private static extern void LoadExtern();

    [DllImport("__Internal")]
    private static extern string GetPlayerData();

    int _scoreCurrent;
    TextMeshProUGUI _textCountCoins;
    TextMeshProUGUI _scoreTextCurrent;
    TextMeshProUGUI _scoreTextBest;
    RawImage _iconUser;
    Texture _photoTexture;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            PlayerInfo = new();
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        #if UNITY_WEBGL && !UNITY_EDITOR
        GetPlayerData();
        Load();
        #endif
    }

    private void Start()
    {
        InitializeReferences();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeReferences();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            _scoreCurrent = (int)GameManager.Instance.PlayerTransform.position.x;
            _scoreTextCurrent.text = $"Score: {_scoreCurrent}";
        }
    }

    public void NewBestScore()
    {
        if (PlayerInfo.BestScore < _scoreCurrent)
        {
            PlayerInfo.BestScore = _scoreCurrent;
            _scoreTextBest.text = $"Best score: {_scoreCurrent}";
        }

        #if UNITY_WEBGL && !UNITY_EDITOR
        Save();
        #endif
    }
    public void AddCoin()
    {
        PlayerInfo.Coins++;
        _textCountCoins.text = PlayerInfo.Coins.ToString();

        #if UNITY_WEBGL && !UNITY_EDITOR
        Save();
        #endif
    }

    public void SetPhoto(string url)
    {
        StartCoroutine(DownloadImage(url));
    }

    IEnumerator DownloadImage(string mediaUrl)
    {
        var request = UnityWebRequestTexture.GetTexture(mediaUrl);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
            Debug.LogWarning(request.error);
        else
        {
            _iconUser.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            _photoTexture = _iconUser.texture;
        }
    }

    private void InitializeReferences()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            _textCountCoins = GameManager.Instance.TextCountCoins;
            _scoreCurrent = (int)GameManager.Instance.PlayerTransform.position.x;
            _scoreTextCurrent = GameManager.Instance.ScoreTextCurrent;
            _scoreTextBest = GameManager.Instance.ScoreTextBest;
            _iconUser = GameManager.Instance.IconUser;

            _iconUser.texture = _iconUser.texture;
            _textCountCoins.text = PlayerInfo.Coins.ToString();
            _scoreTextBest.text = $"Best score: {PlayerInfo.BestScore}";
        }
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(PlayerInfo);
        SaveExtern(json);
    }

    public void Load()
    {
        LoadExtern();
    }

    public void SetPlayerInfo(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            PlayerInfo = JsonUtility.FromJson<PlayerInfo>(value);
        }
        else
        {
            PlayerInfo = new PlayerInfo();
        }
    }
}
