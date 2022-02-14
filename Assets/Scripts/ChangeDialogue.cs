using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDialogue : MonoBehaviour
{
    public InterlocutorDialogue newInterlocutorDialogue;
    public InterlocutorDialogue lastInterlocutorDialogue;
    public PlayerResponses lastPlayerResponse;
    [Tooltip("Dialogue Activator Object")]
    public DialogueActivator objectToChangeDialogue;
    private void Update()
    {
        if (lastInterlocutorDialogue != null)
        {
            if (lastInterlocutorDialogue.isOver)
            {
                SetNewDialogue(newInterlocutorDialogue);
            }
        }
        else if (lastPlayerResponse != null)
        {
            if (lastPlayerResponse.isOver)
            {
                SetNewDialogue(newInterlocutorDialogue);
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
}
