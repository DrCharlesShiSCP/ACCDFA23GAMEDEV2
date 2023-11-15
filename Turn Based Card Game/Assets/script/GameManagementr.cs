using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagementr : MonoBehaviour
{
    public GameObject startCanvas;
    public GameObject instructionsCanvas;
    // Start is called before the first frame update
    void Start()
    {
        startCanvas.SetActive(true);
        instructionsCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DeactivateStartCanvas()
    {
        startCanvas.SetActive(false);
    }
    public void DeactivateInstructionsCanvas()
    {
        instructionsCanvas.SetActive(false);
    }
    public void ActivateInstructionsCanvas()
    {
        instructionsCanvas.SetActive(true);
    }
    public void goToGame()
    {
        SceneManager.LoadScene("BattleScene");
    }
}
