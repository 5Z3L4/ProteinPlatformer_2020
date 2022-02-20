using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatCoinerRunAway : StateMachineBehaviour
{
    public float StopBeforePlayerDistance = 10f;
    public float Speed = 2.5f;

    private Transform _player;
    private Rigidbody2D _rb;
    private void Awake()
    {
        _player = FindObjectOfType<PlayerMovement>().transform;
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("BallsDidBoom", false);
        _rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Mathf.Abs(_player.position.x - _rb.position.x) > StopBeforePlayerDistance)
        {
            animator.SetBool("Jump", true);
        }
        else
        {
            if (_player.position.x - _rb.position.x < 0)
            {
                _rb.velocity = Vector2.right * Time.deltaTime * Speed;
            }
            else
            {
                _rb.velocity = Vector2.left * Time.deltaTime * Speed;
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
