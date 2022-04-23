using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class PlayerBubbleTrigger : MonoBehaviour
{
    public TMP_Text tutorialText;
    public DialogueObject newPlayerBubbleText;
    public DialogueObject tutorial;
    public bool isTutAvailable;
    public bool callItWithoutButton;
    public ShowNormalText normalText;

    private bool isPlayerInTrigger = false;
    private PlayerBubble playerBubble;
    public enum TutorialFinishKey
    {
        None,
        Zoom,
        LeftCtrl,
        Tab,
        S,
        Q,
        Z
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
        if (key == TutorialFinishKey.Zoom && isTutAvailable && (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.T)))
        {
            HideTutorialText();
        }
        if (key == TutorialFinishKey.LeftCtrl && isTutAvailable && (Input.GetKeyDown(KeyCode.LeftControl)))
        {
            HideTutorialText();
        }
        if (key == TutorialFinishKey.Tab && isTutAvailable && (Input.GetKeyDown(KeyCode.Tab)))
        {
            HideTutorialText();
        }
        if (key == TutorialFinishKey.S && isTutAvailable && (Input.GetKeyDown(KeyCode.S)))
        {
            HideTutorialText();
        }
        if (key == TutorialFinishKey.Z && isTutAvailable && (Input.GetKeyDown(KeyCode.Z)))
        {
            HideTutorialText();
            Destroy(gameObject);
        }
        
        if (Input.GetKeyDown(KeyCode.E) || callItWithoutButton)
        {
            if (isPlayerInTrigger)
            {
                ShowTexts();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    private void ShowTexts()
    {
        if (normalText != null)
        {
            normalText.HideText();
        }
        if (tutorial != null && tutorial.Dialogue.Length != 0 && !tutorialText.gameObject.activeInHierarchy && isTutAvailable)
        {
            tutorialText.gameObject.SetActive(true);
            tutorialText.SetText(tutorial.Dialogue[0]);
        }
        if (newPlayerBubbleText != null && newPlayerBubbleText.Dialogue.Length > 0)
        {
            playerBubble.gameObject.SetActive(true);
            playerBubble.SetSizeOfTextBackground(newPlayerBubbleText);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            playerBubble.gameObject.SetActive(false);
        }
    }
    public void HideTutorialText()
    {
        if (tutorialText != null)
        {
            if (tutorialText.gameObject.activeInHierarchy)
            {
                tutorialText.text = string.Empty;
                tutorialText.gameObject.SetActive(false);
                isTutAvailable = false;
                if (normalText != null)
                {
                    normalText.HideText();
                }
            }
        }
    }
}
