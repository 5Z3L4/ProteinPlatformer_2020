using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesCounter : MonoBehaviour
{
    public bool shouldBeCounted;
    public int countAs = 1;
    public bool isSpecificItem;
    // Start is called before the first frame update
    void Start()
    {
        if (shouldBeCounted)
        {
            if (isSpecificItem)
            {
                GameManager.specificLevelItemOnMap += countAs;
            }
            else
            {
                GameManager.collectiblesOnMap += countAs;
            }
        }
    }

}
