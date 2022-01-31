using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RugPullerJump : StateMachineBehaviour
{
    Rigidbody2D rb;
    Transform player;
    public float jumpForce;
    float startHeight;
    public float maxJumpHeight;
    float timer;
    float startTimer = 0.6f;
    float changeAnimTimer;
    float changeAnimStartTimer = 0.2f;
    public float speed = 2.5f;
    Vector2 target;
    bool targetPicked;
    private float jumpDirection;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        startHeight = rb.transform.position.y;
        timer = startTimer;
        changeAnimTimer = changeAnimStartTimer;
        targetPicked = false;
        target = Vector2.zero;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            GetTarget();
            rb.velocity = new Vector2(jumpDirection, 1 * jumpForce);
        }
        else
        {
            timer -= Time.deltaTime;
        }

        if (rb.transform.position.y > maxJumpHeight + startHeight)
        {
            //gdy zaczniesz spadaæ to ju¿ zerujemy
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

    void GetTarget()
    {
        if (!targetPicked)
        {
            if (player.position.x - rb.position.x < 0)
            {
                jumpDirection = -speed;
            }
            else
            {
                jumpDirection = speed;
            }
            targetPicked = true;
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
