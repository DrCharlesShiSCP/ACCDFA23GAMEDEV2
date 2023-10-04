using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public LineRenderer line;

    public PlayerNavMesh nav;

    public bool isBox=false;

    public BoxManager boxManager;

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0,this.transform.position);
        if (Input.GetMouseButton(0))
        {
            line.SetPosition(1, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,60)));
            if (nav != null)
            {
                if (nav.csw.gotSelect)
                {
                    line.enabled = true;
                }
            }
            
            if (isBox&&boxManager.charList.Count>=3)
            {
                line.enabled = true;
            }
        }
        else
        {
            line.enabled = false;
        }
    }
}
