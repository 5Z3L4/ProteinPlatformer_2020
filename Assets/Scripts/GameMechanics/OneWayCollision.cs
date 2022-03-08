using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayCollision : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float waitTime;
    private float goBackTime;
    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
        effector.useColliderMask = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            gameObject.layer = 13;
            waitTime = 0.5f;
        }
        if (waitTime <= 0)
        {
            gameObject.layer = 3;
            
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}
