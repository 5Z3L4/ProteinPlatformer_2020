using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesCounter : MonoBehaviour
{
    public bool shouldBeCounted;
    public int countAs = 1;
    // Start is called before the first frame update
    void Start()
    {
        if (shouldBeCounted)
        {
            GameManager.collectiblesOnMap += countAs;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
