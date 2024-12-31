using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    public int numberOfCoins { get; private set; }

    public UnityEvent<PlayerInventory> onCoinCollected;

    public void CoinCollected()
    {
        numberOfCoins++;
        onCoinCollected.Invoke(this);
    }
}
