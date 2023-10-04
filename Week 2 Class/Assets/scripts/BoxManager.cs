using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoxManager : MonoBehaviour
{
    public List<PlayerNavMesh> charList=new List<PlayerNavMesh>();
    public NavMeshAgent agent;

    public PlayerManager PM;

    // Update is called once per frame
    void Update()
    {
        //Using list compare
        if (charList.Count >= 3)
        {
            agent.enabled = true;
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    //it's 3:30 AM
                    if (hit.collider.CompareTag("Ground"))
                    {
                        //Really should've done it sooner
                        agent.SetDestination(hit.point);
                    }
                }
            }

        }
        else
        {
            agent.enabled = false;
        }
    }
    //Honesty I don't even know if this works or not
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("WinPos"))
        {
            //it works, thank god
            PM.Fire.UnLock();
            PM.Water.UnLock();
            PM.Electric.UnLock();
            print("You Win");
        }
    }
}