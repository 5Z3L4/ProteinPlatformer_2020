using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RugPullerDeath : StateMachineBehaviour
{

    private GameObject dialogue;
    private DialogueUI dialogueUI;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        dialogue = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(g => g.CompareTag("Ball"));
        dialogueUI = GameObject.Find("Canvas").GetComponent<DialogueUI>();
        dialogueUI.isOver = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dialogue.SetActive(true);
        if (dialogueUI.isOver)
        {
            animator.SetBool("BossFlee", true);
        }
    }
}
