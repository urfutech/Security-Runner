using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;


public class Progress : MonoBehaviour
{
    public static Progress Instance { get; private set; }

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
        }
        else
        {
            Destroy(gameObject);
            return;
        }
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
        if (YG2.saves.BestScore < _scoreCurrent)
        {
            YG2.saves.BestScore = _scoreCurrent;
            _scoreTextBest.text = $"Best score: {_scoreCurrent}";
        }

        YG2.SaveProgress();
    }
    public void AddCoin()  // Если прыгнуть на монету, то метод вызовется больше 1 раза
    {
        YG2.saves.Coins++;
        _textCountCoins.text = YG2.saves.Coins.ToString();

        YG2.SaveProgress();
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

            _textCountCoins.text = YG2.saves.Coins.ToString();
            _scoreTextBest.text = $"Best score: {YG2.saves.BestScore}";

            if (_photoTexture == null)
                SetPhoto(YG2.player.photo);
            else
                _iconUser.texture = _photoTexture;
        }
    }
}
