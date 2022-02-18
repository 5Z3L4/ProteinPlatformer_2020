using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatCoinerShoting : StateMachineBehaviour
{
    public float StartTimeBtwShot = 0.5f;
    public int CurrentShotCounter = 0;
    private float _timeBtwShot;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("ShotEnd", false);
        animator.SetBool("Jump", false);
        _timeBtwShot = StartTimeBtwShot;
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (CurrentShotCounter >= 4)
        {
            animator.SetBool("Balls", true);
            CurrentShotCounter = 0;
        }
        if (_timeBtwShot <= 0)
        {
            
            ChoseUpOrDown(animator);
        }
        else
        {
            _timeBtwShot -= Time.deltaTime;
        }
    }

    void ChoseUpOrDown(Animator anim)
    {
        if (Random.value > 0.5f)
        {
            anim.SetBool("ShootUp", true);
        }
        else
        {
            anim.SetBool("ShootDown", true);
        }
        CurrentShotCounter++;
    }
}
