using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunDialogueWithoutInteraction : MonoBehaviour
{
    public DialogueUI dialogueUI;
    public InterlocutorDialogue dialogueToStart;

    private void OnEnable()
    {
        dialogueUI.ShowDialogue(dialogueToStart);
    }
}
