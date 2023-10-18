using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameOver : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public GameObject win1Canvas;
    public GameObject win2Canvas;
    public GameObject win3Canvas;

    void Start()
    {
        gameOverCanvas.SetActive(false);
        win1Canvas.SetActive(false);
        win2Canvas.SetActive(false);
        win3Canvas.SetActive(false);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("lose"))
        {
            ShowGameOverScreen();
        }
        if (collision.gameObject.CompareTag("spike"))
        {
            ShowGameOverScreen();
        }
        if (collision.gameObject.CompareTag("destin"))
        {
            ShowWin1();
        }
        if (collision.gameObject.CompareTag("destin2"))
        {
            ShowWin2();
        }
        if (collision.gameObject.CompareTag("destin3"))
        {
            ShowWin3();
        }
    }

    void ShowGameOverScreen()
    {
        gameOverCanvas.SetActive(true);

        Time.timeScale = 0f;
    }
    void ShowWin1()
    {
        win1Canvas.SetActive(true);

        Time.timeScale = 0f;
    }
    void ShowWin2()
    {
        win2Canvas.SetActive(true);

        Time.timeScale = 0f;
    }
    void ShowWin3()
    {
        win3Canvas.SetActive(true);

        Time.timeScale = 0f;
    }
}
