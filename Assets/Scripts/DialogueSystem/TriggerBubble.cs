using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TriggerBubble : MonoBehaviour
{
    [SerializeField] private GameObject NPCBubble;
    [SerializeField]  private TextBubble textToShow;
    private DialogueUI dialogueUI;
    private DialogueActivator dialogueActivator;
    private Quest questSystem;

    private void Awake()
    {
        questSystem = gameObject.GetComponent<Quest>();
        dialogueActivator = GameObject.Find("Dialogue").GetComponent<DialogueActivator>();
        dialogueUI = GameObject.Find("Canvas").GetComponent<DialogueUI>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (questSystem.isQuestAvailable && !questSystem.isQuestCompleted)
        {
            questSystem.ChangeDialogue();
        }
        NPCBubble.SetActive(true);
        textToShow.BubbleSetup();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        NPCBubble.SetActive(false);
    }
}
