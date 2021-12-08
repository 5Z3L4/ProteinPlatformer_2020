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
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        currentScoreText.text = "Score: " + currentScore.ToString();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Hit");
            PlayPanelAnim();
        }
    }

    private void PlayPanelAnim()
    {
        StartCoroutine(ShowAndClose());
    }
    IEnumerator ShowAndClose()
    {
        animator.SetTrigger("Open");
        yield return new WaitForSeconds(3f);
        animator.SetTrigger("Close");
    }
}
