using UnityEngine;
using UnityEngine.UI;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    public Quest[] questTargets;
    [SerializeField] private DialogueObject startingDialogue;
    private DialogueUI dialogueUI;
    [HideInInspector] public DialogueObject currentDialogue;
    [SerializeField] private GameObject pressToTalk;
    public DialogueObject newDialogue;
    [HideInInspector] public int questID;
    [SerializeField] Sprite imageToShow;
    public Sprite ImageToShow => imageToShow;

    private void Start()
    {
        questID = 0;
        dialogueUI = GameObject.Find("Canvas").GetComponent<DialogueUI>();
        currentDialogue = startingDialogue;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        pressToTalk.SetActive(true);
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerMovement player))
        {
            player.Interactable = this;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        pressToTalk.SetActive(false);
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerMovement player))
        {
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
            if (dialogueUI.IsOpen)
            {
                pressToTalk.SetActive(false);
            }
            else
            {
                pressToTalk.SetActive(true);
            }
        }
    }
    public void Interact(PlayerMovement player)
    {
        if (questTargets != null)
        {
            player.DialogueUI.ShowDialogue(currentDialogue);
        }
        else
        {
            if (!questTargets[questID].isQuestAvailable && !questTargets[questID].isQuestCompleted)
            {
                questTargets[questID].isQuestAvailable = true;
            }
        }
        
    }
}
