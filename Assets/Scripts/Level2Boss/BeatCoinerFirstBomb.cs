using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatCoinerFirstBomb : StateMachineBehaviour
{
    SpawnBalls ballsSpawner;
    private void Awake()
    {
        ballsSpawner = FindObjectOfType<SpawnBalls>();
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("1stBallSpawn", true);
        ballsSpawner.SpawnObjects();
    }
}
