using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public float cameraSpeed = 5f; // Adjust this value to control the camera's follow speed

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.localPosition += new Vector3(0, 0, 1) * Time.deltaTime* cameraSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.localPosition -= new Vector3(0, 0, 1) * Time.deltaTime * cameraSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.localPosition -= new Vector3(1, 0, 0) * Time.deltaTime * cameraSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.localPosition += new Vector3(1, 0, 0) * Time.deltaTime * cameraSpeed;
        }
    }
}
