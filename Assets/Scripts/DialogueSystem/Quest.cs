using UnityEngine;

public class Quest : MonoBehaviour
{
    [HideInInspector]public bool isQuestAvailable;
    [HideInInspector]public bool isQuestCompleted;
    [SerializeField] private GameObject QuestDestination;
    public DialogueObject newDialogue;
    private void Start()
    {
        isQuestAvailable = false;
        isQuestCompleted = false;
    }
    public void ChangeDialogue()
    {
        QuestDestination.GetComponent<DialogueActivator>().currentDialogue = newDialogue;
        isQuestAvailable = false;
        isQuestCompleted = true;
        print("Quest " + QuestDestination.GetComponent<DialogueActivator>().questID + " completed!");
        if (QuestDestination.GetComponent<DialogueActivator>().questID < (QuestDestination.GetComponent<DialogueActivator>().questTargets.Length - 1))
        {
            QuestDestination.GetComponent<DialogueActivator>().questID++;
        }
    }
}
