using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnQuestComplete : MonoBehaviour
{
    public TutorialPages tutorial;
    public Kark npcToMove;
    public ShowNormalText normalText;
    public InterlocutorDialogue interlocutorDialogue;
    public PlayerResponses playerResponse;
    public DialogueActivator karkDialogueActivator;

    private bool _isTutDone = false;
    private void Awake()
    {
        tutorial = FindObjectOfType<TutorialPages>();
    }
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
        tutorial.ActivateTutorialPage(1);
    }

    private void OnDestroy()
    {
        tutorial.DeactivateTutorialPage(1);
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
        if (karkDialogueActivator != null)
        {
            karkDialogueActivator.interactable = false;
        }
    }
}
