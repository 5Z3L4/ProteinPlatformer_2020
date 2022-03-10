using UnityEngine;
using UnityEngine.UI;

public class DialogueActivator : MonoBehaviour
{
    public Quest[] questTargets;
    public InterlocutorDialogue currentDialogue;
    [HideInInspector] public int questID;
    public Sprite ImageToShow => imageToShow;
    public bool interactable = true;
    public bool ActivateWithoutButton = false;
    public ShowNormalText normalText;

    [SerializeField] private InterlocutorDialogue startingDialogue;
    [SerializeField] Sprite imageToShow;
    private DialogueUI dialogueUI;
    private bool isDialogueRunning;
    private bool isPlayerInTrigger;

    private void Start()
    {
        questID = 0;
        dialogueUI = GameObject.Find("Canvas").GetComponent<DialogueUI>();
        currentDialogue = startingDialogue;
    }

    private void Update()
    {
        if (!isPlayerInTrigger || ActivateWithoutButton) return;
        if(Input.GetKeyDown(KeyCode.E) && !dialogueUI.isOpen && interactable)
        {
            Interact();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = true;

            if (isDialogueRunning) return;

            if (ActivateWithoutButton)
            {
                Interact();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerInTrigger = false;
    }
    public void Interact()
    {
        if(normalText != null)
        {
            normalText.HideText();
        }
        isDialogueRunning = true;
        dialogueUI.ChangeinterlocutorSprite(imageToShow);
        if (questTargets == null || questTargets.Length == 0)
        {
            dialogueUI.ShowDialogue(currentDialogue);
        }
        else
        {
            if (!questTargets[questID].isQuestAvailable && !questTargets[questID].isQuestCompleted)
            {
                questTargets[questID].isQuestAvailable = true;
            }
            dialogueUI.ShowDialogue(currentDialogue);
        }
    }
}
