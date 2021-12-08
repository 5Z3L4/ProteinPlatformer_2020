using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    Collectible collectibleObject = new Collectible();
    public Text currentScoreText;
    public static int currentScore;
    public Animator dumbbleAnimator;
    public Animator meatAnimator;
    public Animator proteinAnimator;

    // Update is called once per frame
    void Update()
    {
        currentScoreText.text = "Score: " + currentScore.ToString();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            
        }
    }
}
