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
    //public TextMeshProUGUI playerHPText;
    //public TextMeshProUGUI systemHPText;
    public TextMeshProUGUI playerSelectedCardText;
    public TextMeshProUGUI systemSelectedCardText;
    public TextMeshProUGUI roundNumber;
    public GameObject playerWinCanvas;
    public GameObject sysWinCanvas;
    public GameObject drawCanvas;
    public SimpleHealthBar playerBar;
    public SimpleHealthBar avatarBar;
    public ParticleSystem goldEffect;
    public ParticleSystem woodEffect;
    public ParticleSystem waterEffect;
    public ParticleSystem fireEffect;
    public ParticleSystem earthEffect;
    public TextMeshProUGUI lastRoundText;
    public TextMeshProUGUI interactionCalloutText;
    public TextMeshProUGUI healthCalloutText;




    // HP for player and system
    public int playerHP = 30;
    public int systemHP = 30;
    private int turnCount = 1;
    // Button functions for each element
    public void SelectGold()
    {
        SetPlayerElement(Element.Gold);
        PlayParticleEffect(goldEffect);
    }
    public void SelectWood()
    {
       
        SetPlayerElement(Element.Wood);
        PlayParticleEffect(woodEffect);
  
    }
    public void SelectWater()
    {
        SetPlayerElement(Element.Water);
        PlayParticleEffect(waterEffect);
    }
    public void SelectFire()
    {
        SetPlayerElement(Element.Fire);
        PlayParticleEffect(fireEffect);
    }
    public void SelectEarth()
    {
        SetPlayerElement(Element.Earth);
        PlayParticleEffect(earthEffect);
    }
    private void Start()
    {
        // Update the HP and selected card display at the start
        //UpdateHPDisplay();
        playerBar.UpdateBar(playerHP, 30);
        avatarBar.UpdateBar(systemHP, 30);
        UpdateSelectedCardDisplay();
        playerWinCanvas.SetActive(false);
        sysWinCanvas.SetActive(false);
        drawCanvas.SetActive(false);
        playerSelectedCardText.text = "Player Selection: None";
        systemSelectedCardText.text = "System Selection: None";
        lastRoundText.gameObject.SetActive(false);
    }
    private void SetPlayerElement(Element selectedElement)
    {
        playerSelectedElement = selectedElement;
        ProcessTurn();
    }

    private void ProcessTurn()
    {
        systemSelectedElement = (Element)Random.Range(0, 5);
        if (playerSelectedElement == systemSelectedElement)
        {
            // Display a callout for the same element selection
            interactionCalloutText.text = $"Both chose {playerSelectedElement}! Redo round!";
            interactionCalloutText.gameObject.SetActive(true);

            // Do not increase round count and skip the rest of the turn
            return;
        }
        UpdateHealthPoints();
        playerBar.UpdateBar(playerHP, 30);
        avatarBar.UpdateBar(systemHP, 30);
        //UpdateHPDisplay(); // Update the HP display after each turn
        UpdateSelectedCardDisplay(); // Update the selected cards display
        UpdateRoundisplay();
        DisplayElementalInteraction();
        Debug.Log("Player selected: " + playerSelectedElement);
        Debug.Log("System selected: " + systemSelectedElement);
        Debug.Log("Player HP: " + playerHP + ", System HP: " + systemHP);
        turnCount++;
        if (turnCount >= 11)
        {
            EndGame();
        }
        if (turnCount == 9) // Second-to-last turn (round 10 is the last round)
        {
            lastRoundText.gameObject.SetActive(true);
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
        DisplayHealthCallout();

    }
    private void DisplayHealthCallout()
    {
        if (playerHP == 5 && systemHP > 5)
        {
            healthCalloutText.text = "Player only has 5 HP left!";
            healthCalloutText.gameObject.SetActive(true);
        }
        else if (systemHP == 5 && playerHP > 5)
        {
            healthCalloutText.text = "System only has 5 HP left!";
            healthCalloutText.gameObject.SetActive(true);
        }
        else if (playerHP == 5 && systemHP == 5)
        {
            healthCalloutText.text = "Both Player and System are at critical health!";
            healthCalloutText.gameObject.SetActive(true);
        }
        else
        {
            healthCalloutText.gameObject.SetActive(false);
        }
    }
    /*private void UpdateHPDisplay()
    {
        // Update the text elements to reflect the current HP
        playerHPText.text = "Player HP: " + playerHP;
        systemHPText.text = "System HP: " + systemHP;
    }*/
    private void UpdateSelectedCardDisplay()
    {
        // Update the selected card text elements
        playerSelectedCardText.text = "Player Selected: " + playerSelectedElement;
        systemSelectedCardText.text = "Avatar Selected: " + systemSelectedElement;
    }
    private void UpdateRoundisplay()
    {
        // Update the text elements to reflect the current HP
        roundNumber.text = "Round" + turnCount + "/10";
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
    private void DisplayElementalInteraction()
    {
        string interactionMessage = "";

        if (IsOvercoming(playerSelectedElement, systemSelectedElement))
        {
            interactionMessage = $"{playerSelectedElement} overcomes {systemSelectedElement}!";
        }
        else if (IsOvercoming(systemSelectedElement, playerSelectedElement))
        {
            interactionMessage = $"{systemSelectedElement} overcomes {playerSelectedElement}!";
        }
        else if (IsGenerating(playerSelectedElement, systemSelectedElement))
        {
            interactionMessage = $"{playerSelectedElement} generates {systemSelectedElement}!";
        }
        else if (IsGenerating(systemSelectedElement, playerSelectedElement))
        {
            interactionMessage = $"{systemSelectedElement} generates {playerSelectedElement}!";
        }

        interactionCalloutText.text = interactionMessage;
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
    private void PlayParticleEffect(ParticleSystem particleSystem)
    {
        if (particleSystem != null)
        {
            particleSystem.Play();
        }
    }
    public void restart()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
