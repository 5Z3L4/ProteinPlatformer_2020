using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RugPullerFall : StateMachineBehaviour
{
    Rigidbody2D rb;
    int counter = 1;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.velocity = Vector2.down * 30f;
        counter = animator.GetInteger("JumpCounter");
        animator.SetInteger("JumpCounter", counter + 1);
        counter++;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    if (counter >= 3)
    //    {
    //        animator.SetBool("Rest", true);
    //    }
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetBool("Second"))
        {
            if (counter >= 3)
            {
                animator.SetBool("Rest", true);
                animator.SetInteger("JumpCounter", 0);
            }
        }
        rb.velocity = Vector2.zero;
        animator.SetBool("Fall", false);
    }

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
