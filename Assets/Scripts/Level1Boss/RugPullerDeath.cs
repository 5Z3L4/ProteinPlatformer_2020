using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RugPullerDeath : StateMachineBehaviour
{

    private GameObject dialogue;
    public InterlocutorDialogue lastDialogue;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        dialogue = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(g => g.CompareTag("Ball"));
        lastDialogue = GameObject.Find("LeaveMeAlone").GetComponent<InterlocutorDialogue>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dialogue.SetActive(true);
        if (lastDialogue.isOver)
        {
            animator.SetBool("BossFlee", true);
        }
    }
}
