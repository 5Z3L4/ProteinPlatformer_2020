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

    [SerializeField] public static bool callTimer;
    [SerializeField] public static float time;
    private void Start()
    {
        callTimer = false;
        time = 3.5f;
    }

    void Update()
    {
        if (callTimer)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            else if (time <= 1)
            {
                HideCollected();
                callTimer = false;
            }
        }

        currentScoreText.text = "Score: " + currentScore.ToString();
    }
    
    public void ShowCollected(string name)
    {
        
        GameObject.Find(name).GetComponent<Animator>().SetTrigger("Open");
        print(GameObject.Find(name).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).ToString());
    }
    public void HideCollected()
    {
        GameObject.Find("dumbbleHolder").GetComponent<Animator>().SetTrigger("Close");
        GameObject.Find("meatHolder").GetComponent<Animator>().SetTrigger("Close");
        GameObject.Find("proteinHolder").GetComponent<Animator>().SetTrigger("Close");
    }
}
