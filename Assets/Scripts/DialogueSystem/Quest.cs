using UnityEngine;

public class Quest : MonoBehaviour
{
    [HideInInspector]public bool isQuestAvailable;
    [HideInInspector]public bool isQuestCompleted;
    [Tooltip("Dialogue Activator Object")]
    [SerializeField] private GameObject QuestDestination;
    public DialogueObject newDialogue;
    public bool questItem = false;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!questItem) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            QuestDestination.GetComponent<DialogueActivator>().currentDialogue = newDialogue;
            if (QuestDestination.GetComponent<DialogueActivator>().questID < (QuestDestination.GetComponent<DialogueActivator>().questTargets.Length - 1))
            {
                QuestDestination.GetComponent<DialogueActivator>().questID++;
            }
            Destroy(gameObject);
        }
    }
}
