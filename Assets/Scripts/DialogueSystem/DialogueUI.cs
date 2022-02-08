using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class DialogueUI : MonoBehaviour
{
    
    public Image PlayerImage => playerImage;
    public Image InterlocutorImage => interlocutorImage;
    public Color DefaultInterlocutorColor => defaultInterlocutorColor;
    public Color DefaultPlayerColor => defaultPlayerColor;
    public List<bool> isCompleted = new List<bool>();
    public bool isOpen = false;
    [SerializeField] private Image interlocutorImage;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private Image playerImage;
    private PlayerMovement player;
    private TextWriter textWriter;
    private DialogueActivator dialogueActivator;
    private ResponseHandler responseHandler;
    private Color defaultInterlocutorColor;
    private Color defaultPlayerColor;
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
    private void Start()
    {
        defaultPlayerColor = playerImage.color;
        defaultInterlocutorColor = interlocutorImage.color;
        if (GameObject.Find("Dialogue") != null)
        {
            interlocutorImage.sprite = dialogueActivator.ImageToShow;
        }
    }
    public void ShowDialogue(InterlocutorDialogue interlocutorDialogue)
    {
        isOpen = true;
        player.canMove = false;
        interlocutorImage.gameObject.SetActive(true);
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(interlocutorDialogue));
    }

    public void ChangeinterlocutorSprite(Sprite image)
    {
        interlocutorImage.sprite = image;
    }
    private IEnumerator StepThroughDialogue(InterlocutorDialogue interlocutorDialogue)
    {
        for (int i = 0; i < interlocutorDialogue.interlocutorDialogues.Length; i++)
        {
            string dialogue = interlocutorDialogue.interlocutorDialogues[i];
            yield return RunTypingEffect(dialogue);
            textLabel.text = dialogue;
            if (i == interlocutorDialogue.interlocutorDialogues.Length - 1 && interlocutorDialogue.HasResponses)
            {
                break;
            }
            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        }
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        if (interlocutorDialogue.HasResponses)
        {
            textLabel.text = string.Empty;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
            interlocutorImage.color = DarkenColor(interlocutorImage);
            responseHandler.ShowResponses(interlocutorDialogue.playerResponse);
            if (interlocutorDialogue.playerResponse.Length == 1)
            {
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
                responseHandler.OnPickedResponse(interlocutorDialogue.playerResponse[0]);
            }
        }
        else
        {
            isCompleted.Add(true);
            interlocutorImage.gameObject.SetActive(false);
            CloseDialogueBox();
            
        }
        if (interlocutorDialogue.shouldCheckForEnd)
        {
            interlocutorDialogue.isOver = true;
        }
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        
    }
    public void CloseDialogueBox()
    {
        player.canMove = true;
        playerImage.gameObject.SetActive(false);
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
        isOpen = false;
    }
    private IEnumerator RunTypingEffect(string dialogue)
    {
        textWriter.Run(dialogue, textLabel);
        while (textWriter.IsRunning)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.E))
            {
                textWriter.Stop();
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
            }
        }
    }
    public Color DarkenColor(Image imgToDarken)
    {
        Color darkImg = new Color(imgToDarken.color.r - (imgToDarken.color.r * 0.6f), imgToDarken.color.g - (imgToDarken.color.g * 0.6f), imgToDarken.color.b - (imgToDarken.color.b * 0.6f));
        return darkImg;
    }
}
