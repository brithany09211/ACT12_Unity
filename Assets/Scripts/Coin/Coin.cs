using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float _points = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CoinManager.AddCoin(_points);
        gameObject.SetActive(false);
    }
}
