using UnityEngine;
using TMPro;

public class CoinText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coin;

    private void Awake()
    {
        _coin = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        CoinManager.OnAddPoints += ChangeIuText;
        ChangeIuText();
    }

    private void OnDisable()
    {
        CoinManager.OnAddPoints -= ChangeIuText;
    }

    private void ChangeIuText()
    {
        _coin.text = "Coins: " + CoinManager.Instance.Amount;
    }
}