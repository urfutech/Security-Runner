using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _score;
    [SerializeField] Transform _player;

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            _score.text = $"Ваш счёт: {(int)_player.position.x}";
        }
    }
}
