using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableQuestionmark : MonoBehaviour
{
    public bool shouldCheckForEndOfDialogue;
    public InterlocutorDialogue lastInterlocutorDialogue;
    public PlayerResponses lastPlayerResponse;
    public bool shouldCheckForQuestFinish;
    public Quest quest;

    void Update()
    {
        if (shouldCheckForEndOfDialogue)
        {
            if ((lastInterlocutorDialogue != null && lastInterlocutorDialogue.isOver) || (lastPlayerResponse != null && lastPlayerResponse.isOver))
            {
                gameObject.SetActive(false);
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
