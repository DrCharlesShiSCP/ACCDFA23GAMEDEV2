using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause : MonoBehaviour
{
    public GameObject menuCanvas; // The canvas containing your menu UI
    private bool isPaused = false;

    private void Start()
    {
        if (menuCanvas == null)
        {
            Debug.LogError("Please assign the Menu Canvas in the inspector.");
        }
        menuCanvas.SetActive(false); // Ensuring the menu is hidden on game start
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Using the Escape key to toggle the menu
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

    public void ResumeGame()
    {
        menuCanvas.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void PauseGame()
    {
        menuCanvas.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ReloadGame()
    {
        Time.timeScale = 1f; // Ensuring the game isn't paused when we reload
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

