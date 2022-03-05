using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ResponseHandler : MonoBehaviour
{
    public List<GameObject> tempResponseButtons = new List<GameObject>();

    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;
    private DialogueUI dialogueUI;
    private GameObject currentlySelectedButton;
    public GameObject CurrentlySelectedObject => currentlySelectedButton;
    
    private void Start()
    {
        dialogueUI = gameObject.GetComponent<DialogueUI>();
        EventSystem.current.firstSelectedGameObject = null;
        EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
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
        //if (tempResponseButtons.Count > 1)
        //{
            EventSystem.current.firstSelectedGameObject = tempResponseButtons[0];
            EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
        //}
    }
    public void OnPickedResponse(PlayerResponses response)
    {
        EventSystem.current.firstSelectedGameObject = null;
        EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
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
            if (response.shouldCheckForEnd)
            {
                response.isOver = true;
            }
        }
    }
}

