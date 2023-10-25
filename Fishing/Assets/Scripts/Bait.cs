using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bait : MonoBehaviour
{
    [SerializeField] private Canvas caught;  // Reference to the canvas you want to activate
    private float originalTimeScale;

    private void Start()
    {
        caught.gameObject.SetActive(false);
        if (!caught) Debug.LogError("Please assign the Canvas component in the inspector.");
        originalTimeScale = Time.timeScale;
        caught.gameObject.SetActive(false); // Make sure the canvas is inactive at the start
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            caught.gameObject.SetActive(true);
        }
        PauseGame();
    }
    void PauseGame()
    {
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        Time.timeScale = originalTimeScale;
        caught.gameObject.SetActive(false);
    }
    public void ReloadActiveScene()
    {
        Time.timeScale = 1f; // Reset the timescale
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the active scene
    }
}
