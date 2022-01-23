using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallTutorial : MonoBehaviour
{
    public DialogueUI dialogue;
    public DialogueActivator dialogueAc;
    public PlayerBubbleTrigger tutorial;
    public Kark npcToMove;
    // Start is called before the first frame update
    void Start()
    {
       dialogueAc = GetComponent<DialogueActivator>();
    }

    private void Update()
    {
        if (dialogue.isCompleted.Count == 2)
        {
            if (dialogue.isCompleted[1] && dialogueAc.currentDialogue.Dialogue[0] == "HAHAHA I CAN'T BELIVE YOU FELL FOR THAT!")
            {
                if (!npcToMove.leftOrRight)
                {
                    npcToMove.Flip();
                    npcToMove.leftOrRight = !npcToMove.leftOrRight;
                }
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
