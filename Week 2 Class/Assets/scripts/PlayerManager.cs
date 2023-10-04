using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerNavMesh Fire;
    public PlayerNavMesh Water;
    public PlayerNavMesh Electric;

    public Transform Box;
    public Transform BoxPos1;
    public Transform BoxPos2;
    public Transform BoxPos3;

    public BoxManager BM;
    private void Update()
    {
        
    }

    public Transform SetChar()
    {
        if (BoxPos1.childCount == 0)
        {
            return BoxPos1;
        }
        if (BoxPos2.childCount == 0)
        {
            return BoxPos2;
        }
        if (BoxPos3.childCount == 0)
        {
            return BoxPos3;
        }
        return null;
    }
}
