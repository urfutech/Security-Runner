using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject _shop;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenShop()
    {
        _shop.SetActive(true);
        gameObject.SetActive(false);
    }
}
