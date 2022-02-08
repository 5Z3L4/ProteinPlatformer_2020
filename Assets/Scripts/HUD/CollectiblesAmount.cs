using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectiblesAmount : MonoBehaviour
{
    public TMP_Text itemAmount;
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
            itemAmount.SetText(GameManager.collectedStrenght.ToString() + "/" + GameManager.collectiblesOnMap.ToString());
        }
        else if(specificItem == Items.Meat)
        {
            itemAmount.SetText(GameManager.collectedConstitution.ToString() + "/" + GameManager.collectiblesOnMap.ToString());
        }
        else if(specificItem == Items.Protein)
        {
            itemAmount.SetText(GameManager.collectedAgility.ToString() + "/" + GameManager.collectiblesOnMap.ToString());
        }
    }
}
