using UnityEngine;
using UnityEngine.UI;

public class DialogueActivator : MonoBehaviour
{
    public Quest[] questTargets;
    private DialogueUI dialogueUI;
    public DialogueObject currentDialogue;
    [SerializeField] private DialogueObject startingDialogue;
    [SerializeField] private GameObject pressToTalk;
    [HideInInspector] public int questID;
    [SerializeField] Sprite imageToShow;
    public Sprite ImageToShow => imageToShow;
    public bool Interactable = false;
    public bool ActivateWithoutButton = false;
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
        if(Input.GetKeyDown(KeyCode.E) && !dialogueUI.isOpen)
        {
            Interact();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Interactable)
            {
                pressToTalk.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Interactable)
            {
                pressToTalk.SetActive(false);
            }
            isPlayerInTrigger = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            if (Interactable)
            {
                if (dialogueUI.isOpen)
                {
                    pressToTalk.SetActive(false);
                }
                else
                {
                    pressToTalk.SetActive(true);
                }
            }

            if (isDialogueRunning) return;

            if (ActivateWithoutButton)
            {
                Interact();
            }
        }
    }
    public void Interact()
    {
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
