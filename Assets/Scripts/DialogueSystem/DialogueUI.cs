using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    private SpriteRenderer playerSpriteRen;
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

    public bool IsOpen;
    private void Awake()
    {        
        player = FindObjectOfType<PlayerMovement>();
        if (GameObject.Find("Dialogue") != null)
        {
            dialogueActivator = GameObject.Find("Dialogue").GetComponent<DialogueActivator>();
        }
        playerSpriteRen = GameObject.FindWithTag("PlayerSprite").GetComponent<SpriteRenderer>();
        textWriter = GetComponent<TextWriter>();
        responseHandler = GetComponent<ResponseHandler>();
    }
    void Start()
    {
        playerImage.sprite = playerSpriteRen.sprite;
        playerImage.color = playerSpriteRen.color;
        defaultPlayerColor = playerImage.color;
        defaultInterlocutorColor = interlocutorImage.color;
        if (GameObject.Find("Dialogue") != null)
        {
            interlocutorImage.sprite = dialogueActivator.ImageToShow;
        }
        CloseDialogueBox();
        IsOpen = false;
    }
    public void ShowDialogue(DialogueObject dialogueObject)
    {
        interlocutorImage.gameObject.SetActive(true);
        IsOpen = true;
        player.canMove = false;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    public void ChangeinterlocutorSprite(Sprite image)
    {
        interlocutorImage.sprite = image;
        interlocutorImage.gameObject.transform.localScale *= new Vector2(-1, 1);
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
        CloseDialogueBox();
        if (!dialogueObject.HasResponses)
        {
            IsOpen = false;
        }
        if (dialogueObject.HasResponses)
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
            interlocutorImage.color = DarkenColor(interlocutorImage);
            IsOpen = true;
            responseHandler.ShowResponses(dialogueObject.Responses);
            if (dialogueObject.Responses.Length == 1)
            {
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
                responseHandler.OnPickedResponse(dialogueObject.Responses[0]);
            } 
        }
        else
        {
            interlocutorImage.gameObject.SetActive(false);
            player.canMove = true;
        }
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
        
    }
    public void CloseDialogueBox()
    {
        playerImage.gameObject.SetActive(false);
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
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
