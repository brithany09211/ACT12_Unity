using System;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class CoinManager : MonoBehaviour
{
    private static CoinManager _instance;

    public static CoinManager Instance
    {
        get { return _instance; }
    }

    public static Action OnAddPoints;

    private float _amount;

    public float Amount
    {
        get { return _amount; }
    }

    void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public static void AddCoin(float amount)
    {
        if (_instance != null)
        {
            _instance.AddCoinInternal(amount);
            OnAddPoints?.Invoke();
        }
        else
        {
            Debug.Log("Falta CoinManager en la escena");
        }
    }

    private void AddCoinInternal(float amount)
    {
        _amount += amount;
    }
}
