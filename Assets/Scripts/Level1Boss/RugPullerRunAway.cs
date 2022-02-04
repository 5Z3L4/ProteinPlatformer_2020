using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RugPullerRunAway : StateMachineBehaviour
{
    private Transform player;
    private Rigidbody2D rb;
    public GameObject ticket;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        Instantiate(ticket, animator.transform.position, Quaternion.identity);
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player.transform.position.x - animator.transform.position.x < 0)
        {
            rb.velocity = new Vector2(15, 0);
        }
        else
        {
            rb.velocity = new Vector2(-15, 0);
        }
    }

}
