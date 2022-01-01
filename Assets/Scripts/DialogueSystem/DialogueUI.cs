using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    private TextWriter textWriter;
    private DialogueActivator dialogueActivator;
    private ResponseHandler responseHandler;
    [SerializeField] private Image playerImage;
    public Image PlayerImage => playerImage;
    [SerializeField] private Image interlocutorImage;
    public Image InterlocutorImage => interlocutorImage;

    public bool IsOpen { get; private set; }
    private void Awake()
    {
        dialogueActivator = GameObject.Find("Dialogue").GetComponent<DialogueActivator>();
    }
    void Start()
    {
        playerImage.sprite = GameObject.Find("Player").GetComponentInChildren<SpriteRenderer>().sprite;
        playerImage.color = GameObject.Find("Player").GetComponentInChildren<SpriteRenderer>().color;
        CloseDialogueBox();
        InterlocutorImage.sprite = dialogueActivator.ImageToShow;
        textWriter = GetComponent<TextWriter>();
        responseHandler = GetComponent<ResponseHandler>();
    }
    public void ShowDialogue(DialogueObject dialogueObject)
    {
        IsOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }
    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];
            yield return RunTypingEffect(dialogue);
            textLabel.text = dialogue;
            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses)
            {
                break;
            }
            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
        }
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
        if (dialogueObject.HasResponses)
        {
            CloseDialogueBox();
            responseHandler.ShowResponses(dialogueObject.Responses);
            IsOpen = true;
        }
        else
        {
            CloseDialogueBox();
        }
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
        
    }
    private void CloseDialogueBox()
    {
        IsOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
    private IEnumerator RunTypingEffect(string dialogue)
    {
        textWriter.Run(dialogue, textLabel);
        while (textWriter.IsRunning)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.R))
            {
                textWriter.Stop();
            }
        }
    }

}
