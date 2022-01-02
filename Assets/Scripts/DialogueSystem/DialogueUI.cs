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
    private Color defaultInterlocutorColor;
    private Color defaultPlayerColor;
    public Color DefaultInterlocutorColor => defaultInterlocutorColor;
    public Color DefaultPlayerColor => defaultPlayerColor;

    public bool IsOpen { get; private set; }
    private void Awake()
    {
        dialogueActivator = GameObject.Find("Dialogue").GetComponent<DialogueActivator>();
    }
    void Start()
    { 
        playerImage.sprite = GameObject.Find("Player").GetComponentInChildren<SpriteRenderer>().sprite;
        playerImage.color = GameObject.Find("Player").GetComponentInChildren<SpriteRenderer>().color;
        defaultPlayerColor = playerImage.color;
        defaultInterlocutorColor = interlocutorImage.color;
        CloseDialogueBox();
        interlocutorImage.sprite = dialogueActivator.ImageToShow;
        textWriter = GetComponent<TextWriter>();
        responseHandler = GetComponent<ResponseHandler>();
    }
    public void ShowDialogue(DialogueObject dialogueObject)
    {
        interlocutorImage.gameObject.SetActive(true);
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
            interlocutorImage.color = DarkenColor(interlocutorImage);
            CloseDialogueBox();
            responseHandler.ShowResponses(dialogueObject.Responses);
            IsOpen = true;
        }
        else
        {
            interlocutorImage.gameObject.SetActive(false);
            CloseDialogueBox();
        }
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
        
    }
    private void CloseDialogueBox()
    {
        playerImage.gameObject.SetActive(false);
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
    public Color DarkenColor(Image imgToDarken)
    {
        Color darkImg = new Color(imgToDarken.color.r - (imgToDarken.color.r * 0.6f), imgToDarken.color.g - (imgToDarken.color.g * 0.6f), imgToDarken.color.b - (imgToDarken.color.b * 0.6f));
        return darkImg;
    }
}
