using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnQuestComplete : MonoBehaviour
{
    public PlayerBubbleTrigger tutorial;
    public Kark npcToMove;
    public bool dialogueFinished;
    public ShowNormalText normalText;

    private void Update()
    {
        if (dialogueFinished)
        {
            if (!npcToMove.facingRight && npcToMove != null && normalText != null)
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
    public void EnableTutorial()
    {
        tutorial.isTutAvailable = true;
    }

    private void OnDestroy()
    {
        tutorial.HideTutorialText();
    }
}
