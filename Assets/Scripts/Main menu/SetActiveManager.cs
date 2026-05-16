using UnityEngine;

public class SetActiveManager : MonoBehaviour
{
    [SerializeField] GameObject _menu;
    [SerializeField] GameObject _shop;
    [SerializeField] GameObject _leaderboard;

    private void Start()
    {
        _menu.SetActive(true);
        _shop.SetActive(false);
        _leaderboard.SetActive(false);
    }
}
