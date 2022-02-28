using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Transform ObjToFollow;
    private Camera _miniMapCam;

    private void Awake()
    {
        _miniMapCam = GetComponent<Camera>();
    }
    // Update is called once per frame
    private void Update()
    {
        MapZoom();
    }
    private void LateUpdate()
    {
        FollowObj();
    }
    void MapZoom()
    {
        if (Input.GetKey(KeyCode.LeftBracket))
        {
            _miniMapCam.orthographicSize++;
        }
        else if (Input.GetKey(KeyCode.RightBracket))
        {
            _miniMapCam.orthographicSize--;
        }
    }

    void FollowObj()
    {
        Vector3 newPosition = ObjToFollow.position;
        newPosition.y = transform.position.y;
        newPosition.z = transform.position.z;
        transform.position = newPosition;
    }
}
