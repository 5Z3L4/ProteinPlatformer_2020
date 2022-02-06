using UnityEngine;

public class Quest : MonoBehaviour
{
    [HideInInspector]public bool isQuestAvailable;
    [HideInInspector]public bool isQuestCompleted;
    [Tooltip("Dialogue Activator Object")]
    [SerializeField] private GameObject QuestDestinationDialogueActivator;
    public DialogueObject newDialogue;
    public bool questItem = false;
    private void Start()
    {
        isQuestAvailable = false;
        isQuestCompleted = false;
    }
    public void ChangeDialogue()
    {
        QuestDestinationDialogueActivator.GetComponent<DialogueActivator>().currentDialogue = newDialogue;
        isQuestAvailable = false;
        isQuestCompleted = true;
        print("Quest " + QuestDestinationDialogueActivator.GetComponent<DialogueActivator>().questID + " completed!");
        if (QuestDestinationDialogueActivator.GetComponent<DialogueActivator>().questID < (QuestDestinationDialogueActivator.GetComponent<DialogueActivator>().questTargets.Length - 1))
        {
            QuestDestinationDialogueActivator.GetComponent<DialogueActivator>().questID++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!questItem) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            QuestDestinationDialogueActivator.GetComponent<DialogueActivator>().currentDialogue = newDialogue;
            if (QuestDestinationDialogueActivator.GetComponent<DialogueActivator>().questID < (QuestDestinationDialogueActivator.GetComponent<DialogueActivator>().questTargets.Length - 1))
            {
                QuestDestinationDialogueActivator.GetComponent<DialogueActivator>().questID++;
            }
            Destroy(gameObject);
        }
    }
}
