using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TriggerBubble : MonoBehaviour
{
    [SerializeField] private GameObject NPCBubble;
    [SerializeField]  private TextBubble textToShow;
    private DialogueUI dialogueUI;
    private Quest questSystem;
    private TutorialManagement tutorial;
    private bool _isPlayerInTrigger = false;

    private void Awake()
    {
        if (!GetComponent<TutorialManagement>())
        {
            tutorial = GetComponent<TutorialManagement>();
        }
        questSystem = gameObject.GetComponent<Quest>();
        dialogueUI = GameObject.Find("Canvas").GetComponent<DialogueUI>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isPlayerInTrigger) return;

        if (collision.CompareTag("Player"))
        {
            _isPlayerInTrigger = true;
            if (questSystem != null)
            {
                if (questSystem.isQuestAvailable && !questSystem.isQuestCompleted)
                {
                    questSystem.ChangeDialogue();
                }
            }
            NPCBubble.SetActive(true);
            textToShow.BubbleSetup();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            NPCBubble.SetActive(false);
            textToShow.startingDialogueFinished = true;
            _isPlayerInTrigger = false;
        }
    }
}
