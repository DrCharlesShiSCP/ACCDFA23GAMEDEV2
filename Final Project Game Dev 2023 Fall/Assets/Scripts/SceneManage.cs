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
    public void hazard()
    {
        SceneManager.LoadScene("Hazards");
    }
    public void gravity()
    {
        SceneManager.LoadScene("Gravity Manipulation");
    }
    public void platform()
    {
        SceneManager.LoadScene("Moving Platforms");
    }
    public void menu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void darkness()
    {
        SceneManager.LoadScene("Darkness");
    }
}
