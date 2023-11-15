using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerSelect : MonoBehaviour
{
    public enum Element { Gold, Wood, Water, Fire, Earth }

    private Element playerSelectedElement;
    private Element systemSelectedElement;
    public TextMeshProUGUI playerHPText;
    public TextMeshProUGUI systemHPText;
    public TextMeshProUGUI playerSelectedCardText;
    public TextMeshProUGUI systemSelectedCardText;
    public TextMeshProUGUI roundNumber;
    public GameObject playerWinCanvas;
    public GameObject sysWinCanvas;
    public GameObject drawCanvas;

    // HP for player and system
    public int playerHP = 30;
    public int systemHP = 30;
    private int turnCount = 1;
    // Button functions for each element
    public void SelectGold() => SetPlayerElement(Element.Gold);
    public void SelectWood() => SetPlayerElement(Element.Wood);
    public void SelectWater() => SetPlayerElement(Element.Water);
    public void SelectFire() => SetPlayerElement(Element.Fire);
    public void SelectEarth() => SetPlayerElement(Element.Earth);
    private void Start()
    {
        // Update the HP and selected card display at the start
        UpdateHPDisplay();
        UpdateSelectedCardDisplay();
        playerWinCanvas.SetActive(false);
        sysWinCanvas.SetActive(false);
        drawCanvas.SetActive(false);
}
    private void SetPlayerElement(Element selectedElement)
    {
        playerSelectedElement = selectedElement;
        ProcessTurn();
    }

    private void ProcessTurn()
    {
        systemSelectedElement = (Element)Random.Range(0, 5);
        UpdateHealthPoints();
        UpdateHPDisplay(); // Update the HP display after each turn
        UpdateSelectedCardDisplay(); // Update the selected cards display
        UpdateRoundisplay();
        Debug.Log("Player selected: " + playerSelectedElement);
        Debug.Log("System selected: " + systemSelectedElement);
        Debug.Log("Player HP: " + playerHP + ", System HP: " + systemHP);
        turnCount++;
        if (turnCount >= 11)
        {
            EndGame();
        }
    }

    private void UpdateHealthPoints()
    {
        if (IsOvercoming(playerSelectedElement, systemSelectedElement))
        {
            systemHP = Mathf.Max(0, systemHP - 5);
        }
        else if (IsOvercoming(systemSelectedElement, playerSelectedElement))
        {
            playerHP = Mathf.Max(0, playerHP - 5);
        }
        else if (IsGenerating(playerSelectedElement, systemSelectedElement) || IsGenerating(systemSelectedElement, playerSelectedElement))
        {
            playerHP = Mathf.Min(30, playerHP + 5);
            systemHP = Mathf.Min(30, systemHP + 5);
        }

    }
    private void UpdateHPDisplay()
    {
        // Update the text elements to reflect the current HP
        playerHPText.text = "Player HP: " + playerHP;
        systemHPText.text = "System HP: " + systemHP;
    }
    private void UpdateSelectedCardDisplay()
    {
        // Update the selected card text elements
        playerSelectedCardText.text = "Player Selected: " + playerSelectedElement;
        systemSelectedCardText.text = "System Selected: " + systemSelectedElement;
    }
    private void UpdateRoundisplay()
    {
        // Update the text elements to reflect the current HP
        roundNumber.text = "Round" + turnCount;
    }
    private bool IsGenerating(Element one, Element two)
    {
        return (one == Element.Wood && two == Element.Fire) ||
               (one == Element.Fire && two == Element.Earth) ||
               (one == Element.Earth && two == Element.Gold) ||
               (one == Element.Gold && two == Element.Water) ||
               (one == Element.Water && two == Element.Wood);
    }

    private bool IsOvercoming(Element one, Element two)
    {
        return (one == Element.Gold && two == Element.Wood) ||
               (one == Element.Wood && two == Element.Earth) ||
               (one == Element.Earth && two == Element.Water) ||
               (one == Element.Water && two == Element.Fire) ||
               (one == Element.Fire && two == Element.Gold);
    }
    private void EndGame()
    {
        if (playerHP > systemHP)
        {
            playerWinCanvas.SetActive(true);
        }
        else if (systemHP > playerHP)
        {
            sysWinCanvas.SetActive(true);
        }
        else
        {
            drawCanvas.SetActive(true);
        }
    }
    public void restart()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
