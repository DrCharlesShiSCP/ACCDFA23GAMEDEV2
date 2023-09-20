using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObjects : MonoBehaviour
{
    public Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Clicked()
    {
        renderer.material.color = Color.red;
    }
}
