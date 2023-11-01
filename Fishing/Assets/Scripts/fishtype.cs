using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishtype : MonoBehaviour
{
    [SerializeField] private Canvas caught1;
    [SerializeField] private Canvas caught2;
    [SerializeField] private Canvas caught3;
    // Start is called before the first frame update
    void Start()
    {
        caught1.gameObject.SetActive(false);
        caught2.gameObject.SetActive(false);
        caught3.gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("fishe1"))
        {
            Debug.Log("caught1");
            caught1.gameObject.SetActive(true);
        }
        else if (other.gameObject.CompareTag("fishe2"))
        {
            Debug.Log("caught2");
            caught2.gameObject.SetActive(true);
        }
        else if (other.gameObject.CompareTag("fishe3"))
        {
            Debug.Log("caught3");
            caught3.gameObject.SetActive(true);
        }
    }
}
