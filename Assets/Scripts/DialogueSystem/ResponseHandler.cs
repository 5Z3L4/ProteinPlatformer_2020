using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResponseHandler : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;
    private DialogueUI dialogueUI;
    List<GameObject> tempResponseButtons = new List<GameObject>();
    private void Start()
    {
        dialogueUI = GetComponent<DialogueUI>();
    }
    public void ShowResponses(Response[] responses)
    {
        dialogueUI.PlayerImage.color = dialogueUI.DefaultPlayerColor;
        dialogueUI.PlayerImage.gameObject.SetActive(true);
        foreach (Response response in responses)
        {
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response));
            tempResponseButtons.Add(responseButton);
        }
        responseBox.gameObject.SetActive(true);
    }
    public void OnPickedResponse(Response response)
    {
        dialogueUI.PlayerImage.color = dialogueUI.DarkenColor(dialogueUI.PlayerImage);
        dialogueUI.InterlocutorImage.color = dialogueUI.DefaultInterlocutorColor;
        responseBox.gameObject.SetActive(false);
        foreach (GameObject button in tempResponseButtons)
        {
            Destroy(button);
        }
        tempResponseButtons.Clear();
        dialogueUI.ShowDialogue(response.DialogueObject);
    }
}

