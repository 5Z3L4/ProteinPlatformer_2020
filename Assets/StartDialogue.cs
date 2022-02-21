using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StartDialogue : StateMachineBehaviour
{
    bool isDialogueOver = false;
    private SetActiveObj dialogue;
    private InterlocutorDialogue lastDialogue;
    private float _waitBeforeTrans = 0.5f;
    public string DialogueName;
    public string BoolName;
    public string LastInterlocutorResponseName;
    private void Awake()
    {
        dialogue = GameObject.Find(DialogueName).GetComponent<SetActiveObj>();
        lastDialogue = GameObject.Find(LastInterlocutorResponseName).GetComponent<InterlocutorDialogue>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
            dialogue.ActivateObj();
            if (lastDialogue.isOver)
            {
                isDialogueOver = true;
                if (_waitBeforeTrans <= 0)
                {
                    animator.SetBool(BoolName, true);
                }
                else
                {
                    _waitBeforeTrans -= Time.deltaTime;
                }
            }
            if (!isDialogueOver) return;
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
