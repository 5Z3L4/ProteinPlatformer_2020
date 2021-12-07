using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectiblesAmount : MonoBehaviour
{
    public Text dumbbelAmountText;
    public Text meatAmountText;
    public Text proteinAmountText;
    public static int dumbbleAmount, meatAmount, proteinAmount;
    void Update()
    {
        dumbbelAmountText.text = " X " + dumbbleAmount;
        meatAmountText.text = " X " + meatAmount;
        proteinAmountText.text = " X " + proteinAmount;
    }
}
