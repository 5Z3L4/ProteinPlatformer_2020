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
    public static int dumbbleAmount, meatAmount, proteinAmount;
    void Update()
    {
        if (specificItem == Items.Dumbble)
        {
            itemAmount.text = " X " + dumbbleAmount;
        }
        else if(specificItem == Items.Meat)
        {
            itemAmount.text = " X " + meatAmount;
        }
        else if(specificItem == Items.Protein)
        {
            itemAmount.text = " X " + proteinAmount;
        }
    }
}
