using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWithSelectedObject : MonoBehaviour
{
    public GameObject obj;


    // Update is called once per frame
    void Update()
    {
        if (obj == null)
        {
            Destroy(gameObject);
        }
    }
}
