using UnityEngine;
using UnityEngine.UI;

public class DialogueActivator : MonoBehaviour, IInteractable
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

    private void Start()
    {
        questID = 0;
        dialogueUI = GameObject.Find("Canvas").GetComponent<DialogueUI>();
        currentDialogue = startingDialogue;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerMovement player))
        {
            player.Interactable = this;
            if (Interactable)
            {
                pressToTalk.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerMovement player))
        {
            if (Interactable)
            {
                pressToTalk.SetActive(false);
            }
            if (player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.Interactable = null;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
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
            
        }
    }
    public void Interact(PlayerMovement player)
    {
        player.DialogueUI.ChangeinterlocutorSprite(imageToShow);
        if (questTargets == null || questTargets.Length == 0)
        {
            player.DialogueUI.ShowDialogue(currentDialogue);
        }
        else
        {
            if (!questTargets[questID].isQuestAvailable && !questTargets[questID].isQuestCompleted)
            {
                questTargets[questID].isQuestAvailable = true;
            }
            player.DialogueUI.ShowDialogue(currentDialogue);
        }

    }
}
