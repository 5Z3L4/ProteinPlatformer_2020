using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatfromDestroyer : MonoBehaviour
{
    public GameObject platformDestructionPoint;
    // Start is called before the first frame update
    void Start()
    {
        platformDestructionPoint = GameObject.Find("PlatformDestructionPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isStoryMode)
        {
            if (transform.position.x < platformDestructionPoint.transform.position.x)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
