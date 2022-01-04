using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    Collectible collectibleObject;
    public Text currentScoreText;
    public static int currentScore;
    public Animator dumbbleAnimator;
    public Animator meatAnimator;
    public Animator proteinAnimator;
    public bool callTimer;
    public float time;

    private void Start()
    {
        callTimer = false;
        time = 3.5f;
    }

    void Update()
    {
        if (callTimer && GameManager.isStoryMode)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            else if (time <= 0)
            {
                HideCollected();
                callTimer = false;
                time = 3.5f;
                GameObject.Find("dumbbelHolder").GetComponent<Animator>().SetBool("Open", false);
                GameObject.Find("meatHolder").GetComponent<Animator>().SetBool("Open", false);
                GameObject.Find("proteinHolder").GetComponent<Animator>().SetBool("Open", false);
            }
        }
        currentScore = GameManager.Score;
        currentScoreText.text = "Score: " + currentScore.ToString();
    }

    public void ShowCollected(string name)
    {
        GameObject.Find(name).GetComponent<Animator>().SetBool("Close", false);
        GameObject.Find(name).GetComponent<Animator>().SetBool("Open", true);
        callTimer = true;
    }
    public void HideCollected()
    {
        GameObject.Find("dumbbelHolder").GetComponent<Animator>().SetBool("Close", true);
        GameObject.Find("meatHolder").GetComponent<Animator>().SetBool("Close", true);
        GameObject.Find("proteinHolder").GetComponent<Animator>().SetBool("Close", true);
    }
    public void HideHUD()
    {
        GameObject.Find("DumbbelAmountImage").GetComponent<Image>().enabled = false;
        GameObject.Find("DumbbelAmountImage").GetComponentInChildren<Text>().enabled = false;
        GameObject.Find("MeatAmountImage").GetComponent<Image>().enabled = false;
        GameObject.Find("MeatAmountImage").GetComponentInChildren<Text>().enabled = false;
        GameObject.Find("ProteinAmountImage").GetComponent<Image>().enabled = false;
        GameObject.Find("ProteinAmountImage").GetComponentInChildren<Text>().enabled = false;
        GameObject.Find("currentScoreText").GetComponent<Text>().enabled = false;
    }
    public void ShowHUD()
    {
        GameObject.Find("DumbbelAmountImage").GetComponent<Image>().enabled = true;
        GameObject.Find("DumbbelAmountImage").GetComponentInChildren<Text>().enabled = true;
        GameObject.Find("MeatAmountImage").GetComponent<Image>().enabled = true;
        GameObject.Find("MeatAmountImage").GetComponentInChildren<Text>().enabled = true;
        GameObject.Find("ProteinAmountImage").GetComponent<Image>().enabled = true;
        GameObject.Find("ProteinAmountImage").GetComponentInChildren<Text>().enabled = true;
        GameObject.Find("currentScoreText").GetComponent<Text>().enabled = true;
    }

}
