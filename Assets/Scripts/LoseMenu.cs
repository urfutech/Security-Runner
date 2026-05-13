using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseMenu : MonoBehaviour
{
    TextMeshProUGUI _score;
    Transform _playerTransform;

    private void Start()
    {
        _score = GameManager.Instance.ScoreText;
        _playerTransform = GameManager.Instance.PlayerTransform;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            _score.text = $"Ваш счёт: {(int)_playerTransform.position.x}";
        }
    }
}
