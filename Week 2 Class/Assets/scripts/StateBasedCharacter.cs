using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateBasedCharacter : MonoBehaviour
{
    //list of states
   public enum States
    {
        Idle,
        Patrol,
        Follow,
        Random
    }
    public States currentState;
    public NavMeshAgent agent;
    public Vector3 followPosition;
    //patrol 
    public List<Vector3>
        public float patrolMin
    //Random variables
    public float randomMax = 1f();
    public float randomMin = -1f();

    
    void Update()
    {
        switch (currentState)
        {
            case States.Idle:
                UpdateIdle();
                break;
            case States.Patrol:
                UpdateIdle();
                break;
            case States.Follow:
                UpdateIdle();
                break;
            case States.Random:
                UpdateIdle();
                break;
        }
    }

    void UpdateIdle()
    {
        agent.isStopped = true;
    }
    void UpdateFollow()
    {
        agent.isStopped = false;
        agent.SetDestination(followPosition);
    }
    void UpdatePatrol()
    {
        Vector3 targetPosition = patrolPosition[patrolIndex];
        Vector3 ourPosition = transform.position;
        Vector3 delta = targetPosition - ourPosition;
        if (delta.magnitude < 0.1f)
        {
            patrolIndex++;
            if(UpdatePatrolindex >= patrolPositions.Count)
            {

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
            agent.SetDestination(newRandomPosition)
        }
    }
}
