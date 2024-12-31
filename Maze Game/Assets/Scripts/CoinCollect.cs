using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        PlayerInventory playerInventory = GetComponent<PlayerInventory>();

        if (playerInventory != null && other.transform.CompareTag("Coin"))
        {
            playerInventory.CoinCollected();
            other.gameObject.SetActive( false );
        }
    }
}
