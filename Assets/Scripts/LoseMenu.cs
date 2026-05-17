using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class LoseMenu : MonoBehaviour
{
    TextMeshProUGUI _score;
    Transform _playerTransform;

    private void Start()
    {
        _score = GameManager.Instance.ScoreTextLoseMenu;
        _playerTransform = GameManager.Instance.PlayerTransform;
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            _score.text = $"Ваш счёт: {(int)_playerTransform.position.x}";
        }
    }

    public void RestartLevel()
    {
        YG2.InterstitialAdvShow();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
