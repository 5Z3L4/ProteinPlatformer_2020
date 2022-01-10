using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ZoomOutCamera : MonoBehaviour
{
    CinemachineVirtualCamera vCam;
    public float minCameraSize;
    public float maxCameraSize;
    public float zoomSpeed = 5f;
    private void Start()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
    }
    void Update()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            if (vCam.m_Lens.OrthographicSize <= maxCameraSize)
            {
                vCam.m_Lens.OrthographicSize += zoomSpeed * Time.deltaTime;
            }
        }
        else if(Input.mouseScrollDelta.y < 0)
        {
            if (vCam.m_Lens.OrthographicSize > minCameraSize)
            {
                vCam.m_Lens.OrthographicSize -= zoomSpeed * Time.deltaTime;
            } 
        }
    }
}
