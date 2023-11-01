using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishstop : MonoBehaviour
{
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            rb.isKinematic = false;
        }

    }
}
