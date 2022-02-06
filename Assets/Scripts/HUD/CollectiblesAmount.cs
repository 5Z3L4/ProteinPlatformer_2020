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
            itemAmount.text = GameManager.collectedStrenght.ToString() + "/" + GameManager.collectiblesOnMap.ToString();
        }
        else if(specificItem == Items.Meat)
        {
            itemAmount.text = GameManager.collectedConstitution.ToString() + "/" + GameManager.collectiblesOnMap.ToString();
        }
        else if(specificItem == Items.Protein)
        {
            itemAmount.text = GameManager.collectedAgility.ToString() + "/" + GameManager.collectiblesOnMap.ToString();
        }
    }
}
