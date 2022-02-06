using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallTutorial : MonoBehaviour
{
    public DialogueUI dialogue;
    public DialogueActivator dialogueAc;
    public PlayerBubbleTrigger tutorial;
    public Kark npcToMove;
    public DialogueObject callingDialogue;
    public bool dialogueFinished;
    public ShowNormalText normalText;
    // Start is called before the first frame update
    void Start()
    {
       dialogueAc = GetComponent<DialogueActivator>();
        callingDialogue.tutorial = GetComponent<CallTutorial>();
    }

    private void Update()
    {
        if (dialogueFinished)
        {
            if (!npcToMove.facingRight)
            {
                normalText.shouldDisplayText = false;
                npcToMove.Flip();
                npcToMove.facingRight = true;
            }
            EnableTutorial();
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
