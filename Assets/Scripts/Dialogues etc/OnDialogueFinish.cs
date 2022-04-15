using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDialogueFinish : MonoBehaviour
{
    public Kark npcToMove;
    public ShowNormalText normalText;
    public InterlocutorDialogue interlocutorDialogue;
    public PlayerResponses playerResponse;
    public TutorialPages tutorial;

    private bool _shouldFlip = true;

    private bool _isTutDone;

    private void Update()
    {
        if (_shouldFlip)
        {
            if ((interlocutorDialogue != null && interlocutorDialogue.isOver) || (playerResponse != null && playerResponse.isOver))
            {
                FlipKark();
                _shouldFlip = false;
            }
        }
    }
    private void FlipKark()
    {
        if (npcToMove != null && !npcToMove.facingRight)
        {
            normalText.shouldDisplayText = false;
            npcToMove.Flip();
            npcToMove.facingRight = true;
        }
        if (tutorial != null && !_isTutDone)
        {
            EnableTutorial();
            _isTutDone = true;
        }
        npcToMove.GetComponentInChildren<DialogueActivator>().interactable = false;
    }
    private void EnableTutorial()
    {
        tutorial.ActivateTutorialPage(1);
    }
}
