using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public bool isPaused;
    [SerializeField] private MonoBehaviour cameraController;


    void Start()
    {
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
        AudioListener.pause = false;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) 
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame() 
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        // Unlock cursor
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;

        // Disable Players Camera
        if (cameraController != null)
        {
            cameraController.enabled = false;
        }

        // Disable audio
        AudioListener.pause = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        // Re-enable the camera control script
        if (cameraController != null)
        {
            cameraController.enabled = true;
        }

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Enable audio
        AudioListener.pause = false;
    }

    public void GoToMainMenu() 
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        isPaused = false;
        AudioListener.pause = false;
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
}
