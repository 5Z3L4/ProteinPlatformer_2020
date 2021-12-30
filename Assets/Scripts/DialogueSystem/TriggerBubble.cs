using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TriggerBubble : MonoBehaviour
{
    [SerializeField] private GameObject NPCBubble;
    [SerializeField]  private TextBubble textToShow;
    private DialogueUI dialogueUI;
    private DialogueActivator dialogueActivator;
    public DialogueObject NewDialogue;
    private void Awake()
    {
        dialogueActivator = GameObject.Find("Dialogue").GetComponent<DialogueActivator>();
        dialogueUI = GameObject.Find("Canvas").GetComponent<DialogueUI>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        NPCBubble.SetActive(true);
        textToShow.BubbleSetup();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        dialogueActivator.newDialogue = NewDialogue;
        //NewDialogue.Dialogue.Length;
        NPCBubble.SetActive(false);
    }
}
