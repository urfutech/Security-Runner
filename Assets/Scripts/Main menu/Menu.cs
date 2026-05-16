using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject _shop;
    [SerializeField] GameObject _leaderboard;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenShop()
    {
        _shop.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OpenLeaderboard()
    {
        _leaderboard.SetActive(true);
        gameObject.SetActive(false);
    }
}
