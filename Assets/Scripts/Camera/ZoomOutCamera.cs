using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ZoomOutCamera : MonoBehaviour
{
    CinemachineVirtualCamera vCam;
    float startCameraSize;
    public float maxCameraSize;
    private void Start()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        startCameraSize = vCam.m_Lens.OrthographicSize;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.V))
        {
            if (vCam.m_Lens.OrthographicSize <= maxCameraSize)
            {
                vCam.m_Lens.OrthographicSize += 5f * Time.deltaTime;
            }
        }
        else
        {
            if (vCam.m_Lens.OrthographicSize > startCameraSize)
            {
                vCam.m_Lens.OrthographicSize -= 5f * Time.deltaTime;
            } 
        }
    }
}
