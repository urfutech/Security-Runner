using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] GameObject _menu;

    public void BackToMenu()
    {
        _menu.SetActive(true);
        gameObject.SetActive(false);
    }
}
