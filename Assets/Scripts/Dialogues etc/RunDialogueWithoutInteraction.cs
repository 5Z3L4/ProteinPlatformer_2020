using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunDialogueWithoutInteraction : MonoBehaviour
{
    public DialogueUI dialogueUI;
    public DialogueObject dialogueToStart;

    private void OnEnable()
    {
        dialogueUI.ShowDialogue(dialogueToStart);
    }
}
