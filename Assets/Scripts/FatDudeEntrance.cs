using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FatDudeEntrance : MonoBehaviour
{
    public DialogueActivator dialogueActivator;
    public InterlocutorDialogue[] dialogueData;
    private DialogueUI dialogueBox;

    private void Awake()
    {
        dialogueBox = FindObjectOfType<DialogueUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.collectedSpecificItems == 0)
        {
            dialogueActivator.currentDialogue = dialogueData[0];
        }
        else if (GameManager.collectedSpecificItems <= GameManager.specificLevelItemOnMap/2)
        {
            dialogueActivator.currentDialogue = dialogueData[1];
        }
        else if (GameManager.collectedSpecificItems > GameManager.specificLevelItemOnMap / 2 && GameManager.collectedSpecificItems < GameManager.specificLevelItemOnMap)
        {
            dialogueActivator.currentDialogue = dialogueData[2];
        }
        else
        {
            dialogueActivator.currentDialogue = dialogueData[3];
        }

        //foreach (InterlocutorDialogue dialogue in dialogueData)
        //{
        //    if (dialogue.isOver && !dialogueBox.isOpen)
        //    {
        //        SceneManager.LoadScene("BossLevel2");
        //    }
        //}
    }
}
