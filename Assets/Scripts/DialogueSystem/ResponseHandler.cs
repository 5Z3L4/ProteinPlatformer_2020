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
    public void ShowResponses(Response[] responses)
    {
        dialogueUI.PlayerImage.color = dialogueUI.DefaultPlayerColor;
        dialogueUI.PlayerImage.gameObject.SetActive(true);
        int count = 1;
        foreach(Response response in responses)
        {
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);
            if (responses.Length > 1)
            {
                responseButton.GetComponent<TMP_Text>().text = count + ". " + response.ResponseText;
            }
            else
            {
                responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
            }
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response));
            count++;
            tempResponseButtons.Add(responseButton);
        }
    }
    public void OnPickedResponse(Response response)
    {
        dialogueUI.PlayerImage.color = dialogueUI.DarkenColor(dialogueUI.PlayerImage);
        dialogueUI.InterlocutorImage.color = dialogueUI.DefaultInterlocutorColor;
        foreach (GameObject button in tempResponseButtons)
        {
            Destroy(button);
        }
        tempResponseButtons.Clear();
        if (response.DialogueObject != null)
        {
            dialogueUI.ShowDialogue(response.DialogueObject);
        }
        else
        {
            dialogueUI.CloseDialogueBox();
            dialogueUI.InterlocutorImage.gameObject.SetActive(false);
        }
    }
}

