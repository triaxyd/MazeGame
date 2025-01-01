using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Reference to the parent GameObject that holds the coin prefabs
    public GameObject coinsParent;

    // List to hold references to all the coin GameObjects
    private List<GameObject> allCoins = new List<GameObject>();

    // Number of active coins that need to be collected
    public int totalActiveCoins = 10;

    // Start is called before the first frame update
    void Start()
    {
        // Populate the list with all the coin GameObjects under coinsParent
        foreach (Transform coin in coinsParent.transform)
        {
            allCoins.Add(coin.gameObject);
        }

        // Call the function to randomly select 10 coins
        ActivateRandomCoins(totalActiveCoins);
    }

    // Function to activate a random selection of coins
    void ActivateRandomCoins(int numberOfCoins)
    {
        // Ensure the list contains enough coins to select
        if (allCoins.Count < numberOfCoins)
        {
            Debug.LogError("Not enough coins to activate.");
            return;
        }

        // List to keep track of the selected coin game objects
        List<GameObject> activeCoins = new List<GameObject>();

        // Randomly select 'numberOfCoins' coins without repetition
        while (activeCoins.Count < numberOfCoins)
        {
            // Generate a random index from the allCoins list
            int randomIndex = Random.Range(0, allCoins.Count);

            // Get the coin at that random index
            GameObject selectedCoin = allCoins[randomIndex];

            // Add the coin to the activeCoins list if it's not already selected
            if (!activeCoins.Contains(selectedCoin))
            {
                activeCoins.Add(selectedCoin);
            }
        }

        // Deactivate all coins first
        foreach (GameObject coin in allCoins)
        {
            coin.SetActive(false);
        }

        // Activate the selected random coins
        foreach (GameObject coin in activeCoins)
        {
            coin.SetActive(true);
        }
    }
}
