using UnityEngine;
using UnityEngine.EventSystems;
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
    public bool isOpen = false;
    public GameObject miniMap;
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
    private float jumpBufferTemp;
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
        jumpBufferTemp = player.jumpBuffer;
        defaultPlayerColor = playerImage.color;
        defaultInterlocutorColor = interlocutorImage.color;
        if (GameObject.Find("Dialogue") != null)
        {
            interlocutorImage.sprite = dialogueActivator.ImageToShow;
        }
    }
    private void Update()
    {
        if (isOpen)
        {
            //if (responseHandler.tempResponseButtons.Count > 1)
            //{
                if (EventSystem.current.currentSelectedGameObject == null && responseHandler.tempResponseButtons.Count > 0)
                {
                    EventSystem.current.SetSelectedGameObject(responseHandler.tempResponseButtons[0]);
                }
            //}
        }
    }
    public void ShowDialogue(InterlocutorDialogue interlocutorDialogue)
    {
        if (Time.timeScale == 0) return;
        miniMap.SetActive(false);
        player.jumpBuffer = 0;
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
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        if (interlocutorDialogue.HasResponses)
        {
            textLabel.text = string.Empty;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            interlocutorImage.color = DarkenColor(interlocutorImage);
            responseHandler.ShowResponses(interlocutorDialogue.playerResponse);
            //if (interlocutorDialogue.playerResponse.Length == 1)
            //{
            //    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            //    responseHandler.OnPickedResponse(interlocutorDialogue.playerResponse[0]);
            //}
        }
        else
        {
            CloseDialogueBox();
        }
        if (interlocutorDialogue.shouldCheckForEnd)
        {
            interlocutorDialogue.isOver = true;
        }
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        
        
    }
    public void CloseDialogueBox()
    {
        player.canMove = true;
        playerImage.gameObject.SetActive(false);
        interlocutorImage.gameObject.SetActive(false);
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
        isOpen = false;
        miniMap.SetActive(true);
        textWriter.Stop();
        StopAllCoroutines();
        StartCoroutine(ResetJumpBuffer(1f));
    }
    private IEnumerator RunTypingEffect(string dialogue)
    {
        textWriter.Run(dialogue, textLabel);
        while (textWriter.IsRunning)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                textWriter.Stop();
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
        }
    }
    public Color DarkenColor(Image imgToDarken)
    {
        Color darkImg = new Color(imgToDarken.color.r - (imgToDarken.color.r * 0.6f), imgToDarken.color.g - (imgToDarken.color.g * 0.6f), imgToDarken.color.b - (imgToDarken.color.b * 0.6f));
        return darkImg;
    }
    public IEnumerator ResetJumpBuffer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        player.jumpBuffer = jumpBufferTemp;
    }
}
