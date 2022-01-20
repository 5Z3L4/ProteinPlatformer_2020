using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RugPullerJump : StateMachineBehaviour
{
    Rigidbody2D rb;
    public float jumpForce;
    float startHeight;
    public float maxJumpHeight;
    float timer;
    float startTimer = 0.6f;
    float changeAnimTimer;
    float changeAnimStartTimer = 0.2f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        startHeight = rb.transform.position.y;
        timer = startTimer;
        changeAnimTimer = changeAnimStartTimer;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
        else
        {
            timer -= Time.deltaTime;
        }
        if (rb.transform.position.y > maxJumpHeight + startHeight)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            if (changeAnimTimer <=0)
            {
                animator.SetBool("Fall", true);
                animator.SetBool("Jump", false);
            }
            else
            {
                changeAnimTimer -= Time.deltaTime;
            }
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
