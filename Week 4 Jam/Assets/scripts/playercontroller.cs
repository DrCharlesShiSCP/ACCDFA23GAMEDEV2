using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public GameObject player;
    public float moveAmount = 1.0f;
    public float rollSpeed = 5f;

    void Update()
    {
        Vector3 rollDirection = Vector3.left;

        playerRigidbody.AddForce(rollDirection * rollSpeed, ForceMode.Force);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.transform.position += Vector3.up * moveAmount;
        }
    }
}
