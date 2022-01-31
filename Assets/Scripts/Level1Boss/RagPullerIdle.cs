using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class RagPullerIdle : StateMachineBehaviour
{
    float timer;
    public float startTimer = 2f;
    bool isDialogueOver = false;
    private GameObject dialogue;
    private DialogueUI dialogueUI;
    public bool beforeFirstState;

    private void Awake()
    {
        dialogue = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(g => g.CompareTag("BossIntroduction"));
        dialogueUI = GameObject.Find("Canvas").GetComponent<DialogueUI>();
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = startTimer;
        animator.SetBool("Rest", false);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (beforeFirstState)
        {
            dialogue.SetActive(true);
            if (dialogueUI.isOver)
            {
                isDialogueOver = true;
                animator.SetBool("StateOne", true);
            }
            if (!isDialogueOver) return;
        }


        if (timer <= 0)
        {
            animator.SetBool("Fall", false);
            animator.SetBool("Run", true);
            animator.SetBool("Jump", false);
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
