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
    private void OnPickedResponse(Response response)
    {
        dialogueUI.PlayerImage.color = new Color(dialogueUI.PlayerImage.color.r - (dialogueUI.PlayerImage.color.r * 0.6f), dialogueUI.PlayerImage.color.g - (dialogueUI.PlayerImage.color.g * 0.6f), dialogueUI.PlayerImage.color.b - (dialogueUI.PlayerImage.color.b * 0.6f));
        responseBox.gameObject.SetActive(false);
        foreach (GameObject button in tempResponseButtons)
        {
            Destroy(button);
        }
        tempResponseButtons.Clear();
        dialogueUI.ShowDialogue(response.DialogueObject);
    }
}

