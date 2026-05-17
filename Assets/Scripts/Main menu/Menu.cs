using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject _shop;
    [SerializeField] GameObject _leaderboard;

    public void StartGame()
    {
        YG2.InterstitialAdvShow();
        SceneManager.LoadScene(1);
    }

    public void OpenShop()
    {
        YG2.InterstitialAdvShow();
        _shop.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OpenLeaderboard()
    {
        YG2.InterstitialAdvShow();
        _leaderboard.SetActive(true);
        gameObject.SetActive(false);
    }
}
