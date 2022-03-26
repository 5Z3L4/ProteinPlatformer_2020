using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuscleDogeFireworksLaunch : StateMachineBehaviour
{
    public List<GameObject> Fireworks;
    public GameObject FinishingFirework;
    public float StartCountTimer = 1.5f;
    private float _countTimer = 0;
    private int _fireworkCounter = 0;
    private bool _lastFireWorkSpawned = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _fireworkCounter = 0;
        _lastFireWorkSpawned = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_countTimer <= 0)
        {
            if (!_lastFireWorkSpawned)
            {
                if (_fireworkCounter <= 2)
                {
                    _fireworkCounter++;
                    Instantiate(Fireworks[Random.Range(0, 3)]);
                    _countTimer = StartCountTimer;
                }
                else
                {
                    Instantiate(FinishingFirework);
                    _lastFireWorkSpawned = true;
                    _countTimer = 1.5f;
                }
            }
            else
            {
                animator.SetBool("Fall", true);
            }
           
        }
        else
        {
            _countTimer -= Time.deltaTime;
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
