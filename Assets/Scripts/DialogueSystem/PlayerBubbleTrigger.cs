using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class PlayerBubbleTrigger : MonoBehaviour
{
    private PlayerBubble playerBubble;
    public TMP_Text tutorialText;
    public DialogueObject newPlayerBubbleText;
    public DialogueObject tutorial;
    public bool isTutAvailable;
    public bool callItWithoutButton;
    [SerializeField] private TMP_Text pressE;
    private bool isPlayerInTrigger = false;
    public enum TutorialFinishKey
    {
        None,
        ScrollWheel,
        LeftCtrl,
        Tab,
        S,
        Q
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
        if (key == TutorialFinishKey.ScrollWheel && isTutAvailable && (Input.mouseScrollDelta.y < 0 || Input.mouseScrollDelta.y > 0))
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
        if (key == TutorialFinishKey.Q && isTutAvailable && (Input.GetKeyDown(KeyCode.Q)))
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && pressE != null)
        {
            pressE.gameObject.SetActive(true);
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
        if (pressE != null)
        {
            pressE.gameObject.SetActive(false);
        }
        if (tutorial != null && tutorial.Dialogue.Length != 0 && !tutorialText.gameObject.activeInHierarchy && isTutAvailable)
        {
            tutorialText.gameObject.SetActive(true);
            tutorialText.SetText(tutorial.Dialogue[0]);
        }
        if (newPlayerBubbleText != null && newPlayerBubbleText.Dialogue.Length > 0)
        {
            playerBubble.gameObject.SetActive(true);
            playerBubble.BubbleSetup(newPlayerBubbleText);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (pressE != null)
            {
                pressE.gameObject.SetActive(false);
            }
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
            }
        }

    }
}
