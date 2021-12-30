using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    private DialogueUI dialogueUI;
    [SerializeField] private DialogueObject currentDialogueObject;
    [SerializeField] private GameObject pressToTalk;
    public DialogueObject[] dialogues;

    private void Start()
    {
        dialogueUI = GameObject.Find("Canvas").GetComponent<DialogueUI>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        pressToTalk.SetActive(true);
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerMovement player))
        {
            player.Interactable = this;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        pressToTalk.SetActive(false);
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerMovement player))
        {
            if (player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.Interactable = null;
            }
        }
    }
    public void Interact(PlayerMovement player)
    {
        currentDialogueObject = dialogues[dialogueUI.currentDialogue];
        player.DialogueUI.ShowDialogue(currentDialogueObject);
    }
}
