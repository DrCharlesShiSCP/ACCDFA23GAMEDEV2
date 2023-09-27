using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateBasedCharacter : MonoBehaviour
{
    //list of states
    public enum States
    {
        Idle = 1,
        Patrol = 2,
        Random = 3
    }

    public enum Colors
    {
        Red = 4,
        Blue = 5,
        Yellow = 6
    }

    public States currentState;
    public Colors currentColor;
    public NavMeshAgent agent;
    public Vector3 followPosition;
    //patrol 
    public List<Vector3> patrolPositions;
    public int patrolIndex;
    public float patrolMinDistance = 1f;
    //Random variables
    public float randomMax = 1f;
    public float randomMin = -1f;
    public float randomTimer = 0f;
    public float randomCountdown = 0f;
    void Update()
    {
        // Check for key presses to update state and color
        CheckInput();

        // Handle the character's behavior based on the current state
        HandleState();
    }

    void CheckInput()
    {
        // Check for key presses to update state and color
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            currentState = States.Idle;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            currentState = States.Patrol;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentState = States.Random;
        }

        // Check for key presses to update color
        if (Input.GetKeyDown(KeyCode.Alpha4)) 
        {
            currentColor = Colors.Red;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5)) 
        {
            currentColor = Colors.Blue;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6)) 
        {
            currentColor = Colors.Yellow;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateStateBasedOnColor(currentColor);
        }
    }

    void HandleState()
    {
        switch (currentState)
        {
            case States.Idle:
                UpdateIdle();
                break;
            case States.Patrol:
                UpdatePatrol();
                break;
            /*case States.Follow:
                UpdateFollow();
                break;*/
            case States.Random:
                UpdateRandom();
                break;
        }
    }

    void UpdateIdle()
    {
        agent.isStopped = true;
    }
    /*void UpdateFollow()
    {
        agent.isStopped = false;
        agent.SetDestination(followPosition);
    }*/
    void UpdatePatrol()
    {
        Vector3 targetPosition = patrolPositions[patrolIndex];
        Vector3 ourPosition = transform.position;
        Vector3 delta = targetPosition - ourPosition;
        if (delta.magnitude < 0.1f)
        {
            patrolIndex++;
            if(patrolIndex >= patrolPositions.Count)
            {
                patrolIndex = 0;
            }
        }
    }
    void UpdateRandom()
    {
        agent.isStopped = false;
        randomTimer += Time.deltaTime;
        if (randomTimer >= randomCountdown)
        {
            randomTimer = 0;
            float randomX = Random.Range(randomMin, randomMax);
            float randomZ = Random.Range(randomMin, randomMax);

            Vector3 newRandomPosition = new Vector3(randomX, randomZ);
            agent.SetDestination(newRandomPosition);
        }
    }
    void UpdateStateBasedOnColor(Colors color)
    {
        StateBasedCharacter[] characters = FindObjectsOfType<StateBasedCharacter>();
        foreach (StateBasedCharacter character in characters)
        {
            if (character.currentColor == color)
            {
                character.currentState = currentState;
            }
        }
    }
}
