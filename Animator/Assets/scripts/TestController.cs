using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public Animator animator;
    public string BouncetriggerName = "goBounce";
    public string SquishBoolName = "isSquish";
    public float dummyValue = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            animator.SetBool(SquishBoolName, true);
        }else if(Input.GetKeyUp(KeyCode.Space)) 
        {
            animator.SetBool(SquishBoolName, false);
        }
    }
}
