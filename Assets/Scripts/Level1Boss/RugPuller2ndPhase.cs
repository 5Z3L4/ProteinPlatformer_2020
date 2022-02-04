using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RugPuller2ndPhase : StateMachineBehaviour
{
    private GameObject dialogue;
    private DialogueUI dialogueUI;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dialogue = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(g => g.CompareTag("BossRage"));
        dialogueUI = GameObject.Find("Canvas").GetComponent<DialogueUI>();
        dialogueUI.isOver = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dialogue.SetActive(true);
        if (dialogueUI.isOver)
        {
            animator.SetBool("RageRun", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
