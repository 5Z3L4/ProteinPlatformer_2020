using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnQuestComplete : MonoBehaviour
{
    public PlayerBubbleTrigger tutorial;
    public Kark npcToMove;
    public ShowNormalText normalText;
    public InterlocutorDialogue interlocutorDialogue;
    public PlayerResponses playerResponse;

    private void Update()
    {
        if (interlocutorDialogue != null && interlocutorDialogue.isOver)
        {
            FlipKark();
        }
        if (playerResponse != null && playerResponse.isOver)
        {
            FlipKark();
        }
    }
    public void EnableTutorial()
    {
        tutorial.isTutAvailable = true;
    }

    private void OnDestroy()
    {
        tutorial.HideTutorialText();
    }
    private void FlipKark()
    {
        if (npcToMove != null && !npcToMove.facingRight)
        {
            normalText.shouldDisplayText = false;
            npcToMove.Flip();
            npcToMove.facingRight = true;
        }
        if (tutorial != null)
        {
            EnableTutorial();
        }
    }
}
