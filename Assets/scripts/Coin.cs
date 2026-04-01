using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    void Update()
    {
        transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter()
    {
        FindAnyObjectByType<CoinManager>().AddOne();
        Destroy(gameObject);
    }
}
