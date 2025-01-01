using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    public void OnTriggerEnter(Collider other)
    {
        PlayerInventory playerInventory = GetComponent<PlayerInventory>();

        if (playerInventory != null && other.transform.CompareTag("Coin"))
        {
            playerInventory.CoinCollected();
            other.gameObject.SetActive( false );
            audioManager.PlaySFX(audioManager.coinCollect);
        }
    }
}
