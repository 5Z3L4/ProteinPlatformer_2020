using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRag : MonoBehaviour
{
    public RugPullerController rp;

    private void Update()
    {
        transform.position = new Vector2(rp.transform.position.x, transform.position.y);
    }
}
