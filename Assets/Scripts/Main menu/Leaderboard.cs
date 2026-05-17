using UnityEngine;
using YG;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] GameObject _menu;

    public void BackToMenu()
    {
        YG2.InterstitialAdvShow();
        _menu.SetActive(true);
        gameObject.SetActive(false);
    }
}
