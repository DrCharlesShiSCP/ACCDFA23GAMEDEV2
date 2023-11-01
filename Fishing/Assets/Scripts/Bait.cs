using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bait : MonoBehaviour
{
    private float originalTimeScale;

    private void Start()
    {
        originalTimeScale = Time.timeScale;
    }
    void PauseGame()
    {
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        Time.timeScale = originalTimeScale;
    }
    public void ReloadActiveScene()
    {
        Time.timeScale = 1f; // Reset the timescale
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the active scene
    }
}
