using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableQuestionmark : MonoBehaviour
{
    public bool shouldCheckForEndOfDialogue;
    public InterlocutorDialogue[] lastInterlocutorDialogue;
    public PlayerResponses[] lastPlayerResponse;
    public bool shouldCheckForQuestFinish;
    public Quest quest;

    void Update()
    {
        if (shouldCheckForEndOfDialogue)
        {
            if (lastInterlocutorDialogue != null && lastInterlocutorDialogue.Length > 0)
            {
                foreach (InterlocutorDialogue interlocutorToCheck in lastInterlocutorDialogue)
                {
                    if (interlocutorToCheck.isOver)
                    {
                        gameObject.SetActive(false);
                    }
                }
            }
            else if (lastPlayerResponse != null && lastPlayerResponse.Length > 0)
            {
                foreach (PlayerResponses playerToCheck in lastPlayerResponse)
                {
                    if (playerToCheck.isOver)
                    {
                        gameObject.SetActive(false);
                    }
                }
            }
        }
        if (shouldCheckForQuestFinish)
        {
            if (quest.isQuestCompleted)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
