using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

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
    PlayerMovement player;
    public List<bool> isCompleted = new List<bool>();
    public bool isOpen = false;
    public bool isOver;
    private void Awake()
    {        
        player = FindObjectOfType<PlayerMovement>();
        if (GameObject.Find("Dialogue") != null)
        {
            dialogueActivator = GameObject.Find("Dialogue").GetComponent<DialogueActivator>();
        }
        textWriter = GetComponent<TextWriter>();
        responseHandler = GetComponent<ResponseHandler>();
    }
    void Start()
    {
        defaultPlayerColor = playerImage.color;
        defaultInterlocutorColor = interlocutorImage.color;
        if (GameObject.Find("Dialogue") != null)
        {
            interlocutorImage.sprite = dialogueActivator.ImageToShow;
        }
       // CloseDialogueBox();
    }
    public void ShowDialogue(DialogueObject dialogueObject)
    {
        isOpen = true;
        player.canMove = false;
        interlocutorImage.gameObject.SetActive(true);
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    public void ChangeinterlocutorSprite(Sprite image)
    {
        interlocutorImage.sprite = image;
        //interlocutorImage.gameObject.transform.localScale *= new Vector2(-1, 1);
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
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
        }
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
        if (dialogueObject.HasResponses)
        {
            textLabel.text = string.Empty;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
            interlocutorImage.color = DarkenColor(interlocutorImage);
            responseHandler.ShowResponses(dialogueObject.Responses);
            if (dialogueObject.Responses.Length == 1)
            {
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
                responseHandler.OnPickedResponse(dialogueObject.Responses[0]);
            }
        }
        else
        {
            isCompleted.Add(true);
            interlocutorImage.gameObject.SetActive(false);
            CloseDialogueBox();
            if (dialogueObject.tutorial != null)
            {
                dialogueObject.SetBool();
            } 
        }
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
        
    }
    public void CloseDialogueBox()
    {
        player.canMove = true;
        playerImage.gameObject.SetActive(false);
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
        isOpen = false;
        isOver = true;
        print(isOver);
    }
    private IEnumerator RunTypingEffect(string dialogue)
    {
        textWriter.Run(dialogue, textLabel);
        while (textWriter.IsRunning)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                textWriter.Stop();
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
            }
        }
    }
    public Color DarkenColor(Image imgToDarken)
    {
        Color darkImg = new Color(imgToDarken.color.r - (imgToDarken.color.r * 0.6f), imgToDarken.color.g - (imgToDarken.color.g * 0.6f), imgToDarken.color.b - (imgToDarken.color.b * 0.6f));
        return darkImg;
    }
}
