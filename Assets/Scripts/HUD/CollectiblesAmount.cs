using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectiblesAmount : MonoBehaviour
{
    public Text itemAmount;
    public enum Items
    {
        Dumbble,
        Meat,
        Protein
    }
    public Items specificItem;
    void Update()
    {
        if (specificItem == Items.Dumbble)
        {
            itemAmount.text = " X " + GameManager.collectedStrenght;
        }
        else if(specificItem == Items.Meat)
        {
            itemAmount.text = " X " + GameManager.collectedConstitution;
        }
        else if(specificItem == Items.Protein)
        {
            itemAmount.text = " X " + GameManager.collectedAgility;
        }
    }
}
