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

    public bool callTimer;
    public float time;

    // Update is called once per frame
    void Update()
    {
        if (callTimer)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            else if (time <= 0)
            {
                HideCollected();
            }
        }
        currentScoreText.text = "Score: " + currentScore.ToString();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ShowCollected("dumbbleHolder");
            ShowCollected("meatHolder");
            ShowCollected("proteinHolder");
        }
    }
    private void Start()
    {
        time = 3f;
    }
    public void ShowCollected(string name)
    {
        print(callTimer);
        callTimer = true;
        print(callTimer);
        GameObject.Find(name).GetComponent<Animator>().SetTrigger("Open");
        
    }
    public void HideCollected()
    {
        GameObject.Find("dumbbleHolder").GetComponent<Animator>().SetTrigger("Close");
        GameObject.Find("meatHolder").GetComponent<Animator>().SetTrigger("Close");
        GameObject.Find("proteinHolder").GetComponent<Animator>().SetTrigger("Close");
    }
}
