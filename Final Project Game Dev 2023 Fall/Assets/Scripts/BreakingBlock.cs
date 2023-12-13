using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingBlock : MonoBehaviour
{
    public float timeToBreak = 3f;
    private float timer = 0f;
    private bool playerOnBlock = false;
    void Update()
    {
        if (playerOnBlock)
        {
            timer += Time.deltaTime;

            if (timer >= timeToBreak)
            {
                gameObject.SetActive(false); // Set the block inactive
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnBlock = true;
        }
    }

    /*void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Reset timer and flag if the player leaves the block before it breaks
            playerOnBlock = false;
            timer = 0f;
        }
    }*/
}
