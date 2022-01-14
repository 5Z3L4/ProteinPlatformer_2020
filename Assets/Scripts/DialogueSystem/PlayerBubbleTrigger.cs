using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class PlayerBubbleTrigger : MonoBehaviour
{
    private PlayerBubble playerBubble;
    private TMP_Text tutorialText;
    public DialogueObject newPlayerBubbleText;
    public DialogueObject tutorial;
    public enum TutorialFinishKey
    {
        ScrollWheel = KeyCode.mouseScrollDelta;
    }
    public TutorialFinishKey key;
    private void Awake()
    {
        tutorialText = GameObject.Find("TutorialText").GetComponent<TMP_Text>();
        playerBubble = GameObject.Find("PlayerBubble").GetComponent<PlayerBubble>();
    }
    private void Start()
    {
        tutorialText.gameObject.SetActive(false);
        playerBubble.gameObject.SetActive(false);
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        if (tutorial != null && tutorial.Dialogue.Length != 0 && !tutorialText.gameObject.activeInHierarchy)
    //        {
    //            tutorialText.gameObject.SetActive(true);
    //            tutorialText.SetText(tutorial.Dialogue[0]);
    //        }
    //        playerBubble.gameObject.SetActive(true);
    //        playerBubble.BubbleSetup(newPlayerBubbleText);
    //    }

    //}
    private void Update()
    {
        if (Input.mouseScrollDelta.y < 0 || Input.mouseScrollDelta.y > 0)
        {
            if (tutorialText.gameObject.activeInHierarchy)
            {
                tutorialText.text = string.Empty;
                tutorialText.gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (tutorial != null && tutorial.Dialogue.Length != 0 && !tutorialText.gameObject.activeInHierarchy)
            {
                tutorialText.gameObject.SetActive(true);
                tutorialText.SetText(tutorial.Dialogue[0]);
            }
            playerBubble.gameObject.SetActive(true);
            playerBubble.BubbleSetup(newPlayerBubbleText);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerBubble.gameObject.SetActive(false);
        }
    }
}
