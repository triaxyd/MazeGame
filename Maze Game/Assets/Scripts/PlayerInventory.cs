using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    public int numberOfCoins { get; private set; }
    public UnityEvent<PlayerInventory> onCoinCollected;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }


    public void CoinCollected()
    {
        numberOfCoins++;
        onCoinCollected.Invoke(this);
        gameManager.CoinCollected();
    }
}
