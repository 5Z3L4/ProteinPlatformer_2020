using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDialogue : MonoBehaviour
{
    public InterlocutorDialogue newInterlocutorDialogue;
    public InterlocutorDialogue[] lastInterlocutorDialogue;
    public PlayerResponses[] lastPlayerResponse;
    [Tooltip("Dialogue Activator Object")]
    public DialogueActivator objectToChangeDialogue;
    private void Update()
    {
        if (lastInterlocutorDialogue != null && lastInterlocutorDialogue.Length > 0)
        {
            foreach(InterlocutorDialogue interlocutorToCheck in lastInterlocutorDialogue)
            {
                if (interlocutorToCheck.isOver)
                {
                    SetNewDialogue(newInterlocutorDialogue);
                    ActivateWithoutButtonChange();
                }
            }
        }
        else if (lastPlayerResponse != null && lastPlayerResponse.Length > 0)
        {
            foreach(PlayerResponses playerToCheck in lastPlayerResponse)
            {
                if (playerToCheck.isOver)
                {
                    SetNewDialogue(newInterlocutorDialogue);
                    ActivateWithoutButtonChange();
                }
            }
        }
    }

    private void SetNewDialogue(InterlocutorDialogue newIntDial)
    {
        if (newInterlocutorDialogue != null)
        {
            objectToChangeDialogue.currentDialogue = newIntDial;
        }
        this.enabled = false;
    }

    private void ActivateWithoutButtonChange()
    {
        objectToChangeDialogue.ActivateWithoutButton = false;
    }
}
