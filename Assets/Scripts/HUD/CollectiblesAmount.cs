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
        VendingMachine,
        Meat,
        Chest
    }
    public Items specificItem;
    void Update()
    {
        if (specificItem == Items.Chest)
        {
            itemAmount.SetText(GameManager.collectedChests.ToString() + "/" + GameManager.chestsOnMap.ToString());
        }
        else if(specificItem == Items.Meat)
        {
            itemAmount.SetText(GameManager.collectedConstitution.ToString() + "/" + GameManager.collectiblesOnMap.ToString());
        }
        else if(specificItem == Items.VendingMachine)
        {
            itemAmount.SetText(GameManager.collectedVendingMachines.ToString() + "/" + GameManager.vendingMachinesOnMap.ToString());
        }
    }
}
