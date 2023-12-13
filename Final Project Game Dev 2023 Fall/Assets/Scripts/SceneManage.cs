using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadHazardsScene()
    {
        SceneManager.LoadScene("Hazards");
    }
    public void LoadGravityScene()
    {
        SceneManager.LoadScene("Gravity Manipulation");
    }
    public void LoadDrop()
    {
        SceneManager.LoadScene("Drop");
    }
    public void LoadChallenge2()
    {
        SceneManager.LoadScene("Challenge2");
    }
    public void LoadChallenge3()
    {
        SceneManager.LoadScene("Challenge3");
    }
    public void LoadMovingPlatScene()
    {
        SceneManager.LoadScene("Moving Platforms");
    }
    public void LoadChallenge()
    {
        SceneManager.LoadScene("Challenge1");
    }
    public void LoadDarkness()
    {
        SceneManager.LoadScene("Darkness");
    }
    public void LoadChallenge4()
    {
        SceneManager.LoadScene("Challenge4");
    }
}
