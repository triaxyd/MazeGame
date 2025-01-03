using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Coin Management")]
    public GameObject coinsParent; // Parent GameObject holding the coins
    private List<GameObject> allCoins = new List<GameObject>();
    public int totalActiveCoins = 2;
    private int collectedCoins = 0;

    [Header("UI References")]
    public GameObject gameOverMenu; // Reference to the Game Over UI
    public GameObject pauseMenu; // Optional pause menu reference
    public GameObject gameWonMenu;
    public TMPro.TextMeshProUGUI gameOverText; // Reference to the "YOU LOST" text
    public GameObject player; // Reference to the player GameObject

    private bool playerDied = false;
    private bool playerWon = false;
  
    private AudioManager audioManager;


    void Start()
    {
        // Initialize AudioManager
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        // Populate the list with all the coin GameObjects under coinsParent
        foreach (Transform coin in coinsParent.transform)
        {
            allCoins.Add(coin.gameObject);
        }

        // Activate random coins at the start
        ActivateRandomCoins(totalActiveCoins);

        // Ensure Game Over menu is initially hidden
        gameOverMenu.SetActive(false);
        gameWonMenu.SetActive(false);
        
    }

    void Update()
    {
        // Check if all coins are collected and player has won
        if (!playerWon && collectedCoins >= totalActiveCoins)
        {
            PlayerWon();
        }
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

    // This method will be called when the player collects a coin
    public void CoinCollected()
    {
        collectedCoins++;
    }


    public void PlayerDied()
    {
        if (playerDied || playerWon) return; // Prevent multiple calls
        
        playerDied = true;

        // Show the "YOU LOST" message
        gameOverText.text = "YOU DIED!";
        gameOverMenu.SetActive(true);

        // Stop the game time
        Time.timeScale = 0f;

        // Stop all background sounds
        if (audioManager != null)
        {
            audioManager.StopLoopingSFX(); 
        }

        // Unlock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Optionally, stop any movement sounds or music playing here

        // Play any additional death animations or sounds
        Animator playerAnimator = player.GetComponent<Animator>();
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("PlayerDie");
        }
    }


    // This method will be called when the player wins
    public void PlayerWon()
    {
        if (playerWon || playerDied) return; // Prevent multiple calls
        playerWon = true;

        // Show the "YOU WON" message
        gameWonMenu.SetActive(true);

        // Stop the game time
        Time.timeScale = 0f;

        // Stop all background sounds
        if (audioManager != null)
        {
            audioManager.StopLoopingSFX(); // Stop any looping sounds
        }

        // Unlock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Play any additional death animations or sounds
        Animator playerAnimator = player.GetComponent<Animator>();
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("PlayerWon");
        }


    }

    // Retry the game
    public void RetryGame()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Go to Main Menu
    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene("MainMenu");
    }
}

