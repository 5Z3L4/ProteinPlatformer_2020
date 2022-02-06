using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResponseHandler : MonoBehaviour
{
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;
    private DialogueUI dialogueUI;
    List<GameObject> tempResponseButtons = new List<GameObject>();
    private void Start()
    {
        dialogueUI = gameObject.GetComponent<DialogueUI>();
    }
    public void ShowResponses(PlayerResponses[] responses)
    {
        dialogueUI.PlayerImage.color = dialogueUI.DefaultPlayerColor;
        dialogueUI.PlayerImage.gameObject.SetActive(true);
        int count = 1;
        foreach(PlayerResponses response in responses)
        {
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);
            if (responses.Length > 1)
            {
                responseButton.GetComponent<TMP_Text>().text = count + ". " + response.responses;
            }
            else
            {
                responseButton.GetComponent<TMP_Text>().text = response.responses;
            }
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response));
            count++;
            tempResponseButtons.Add(responseButton);
        }
    }
    public void OnPickedResponse(PlayerResponses response)
    {
        dialogueUI.PlayerImage.color = dialogueUI.DarkenColor(dialogueUI.PlayerImage);
        dialogueUI.InterlocutorImage.color = dialogueUI.DefaultInterlocutorColor;
        foreach (GameObject button in tempResponseButtons)
        {
            Destroy(button);
        }
        tempResponseButtons.Clear();
        if (response.interlocutorDialogue != null)
        {
            dialogueUI.ShowDialogue(response.interlocutorDialogue);
        }
        else
        {
            dialogueUI.CloseDialogueBox();
            dialogueUI.InterlocutorImage.gameObject.SetActive(false);
        }
    }
}

