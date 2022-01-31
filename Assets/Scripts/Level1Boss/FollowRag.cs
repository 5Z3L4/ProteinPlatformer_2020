using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRag : MonoBehaviour
{
    public RugPullerController rp;
    public bool followOnY = false;
    private void Update()
    {
        
        if (followOnY)
        {
            transform.position = new Vector2(rp.transform.position.x, rp.transform.position.y);
        }
        else
        {
            transform.position = new Vector2(rp.transform.position.x, transform.position.y);
        }
    }
}
