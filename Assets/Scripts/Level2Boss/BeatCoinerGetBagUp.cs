using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatCoinerGetBagUp : StateMachineBehaviour
{
    private BeatCoinerController _beatCoiner;
    private bool _shouldCount = false;
    private float _timer = 3f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _beatCoiner = FindObjectOfType<BeatCoinerController>();
        _beatCoiner.CanGetHit = true;
        animator.SetBool("Jump", false);
        _shouldCount = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_shouldCount)
        {
            if (_timer < 0)
            {
                _beatCoiner.GetBagUp();
                _timer = 3f;
            }
            else
            {
                _timer -= Time.deltaTime;
            }
        }
        Debug.Log(_timer);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _shouldCount = false;
        _timer = 3f;
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
