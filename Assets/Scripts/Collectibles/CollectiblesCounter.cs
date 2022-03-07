using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesCounter : MonoBehaviour
{
    public bool shouldBeCounted;
    public int countAs = 1;
    public bool isSpecificItem;
    public CollectibleTypes CollectibleType = CollectibleTypes.None;
    public enum CollectibleTypes
    {
        Chest,
        VendingMachine,
        None
    }
    
    // Start is called before the first frame update
    void Start()
    {
        if (shouldBeCounted)
        {
            if (isSpecificItem)
            {
                GameManager.specificLevelItemOnMap += countAs;
            }
            else if (CollectibleType == CollectibleTypes.Chest)
            {
                GameManager.chestsOnMap++;
            }
            else if (CollectibleType == CollectibleTypes.VendingMachine)
            {
                GameManager.vendingMachinesOnMap++;
            }
            else
            {
                GameManager.collectiblesOnMap += countAs;
            }
        }
    }

    //Debug only
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Z))
    //    {
    //        print("Chests: " + GameManager.chestsOnMap);
    //        print("Meat: " + GameManager.collectiblesOnMap);
    //    }
    //}

}
