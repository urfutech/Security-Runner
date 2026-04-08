using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;

    [SerializeField] Transform _player;

    void Start()
    {
        ScoreText.text = "Счёт: 0";
    }

    void Update()
    {
        ScoreText.text = $"Счёт: {(int)_player.position.x}";
    }
}
