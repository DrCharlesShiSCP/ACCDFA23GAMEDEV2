using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class menu : MonoBehaviour
{
    public GameObject menuPanel;

    void Start()
    {

        menuPanel.SetActive(false);
    }

    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("escescesc");
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);

        Time.timeScale = (menuPanel.activeSelf) ? 0f : 1f;
    }

    public void Retry()
    {
        SceneManager.LoadScene("MainGame");
    }
}
