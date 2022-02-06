using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interact : MonoBehaviour
{
    public TMP_Text tmpTextObject;
    public string textToShow;
    public bool playerWantsToInteract;
    private bool playerInTrigger;
    private void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E) && !playerWantsToInteract)
        {
            playerWantsToInteract = true;
            if (!string.IsNullOrEmpty(tmpTextObject.text))
            {
                tmpTextObject.text = string.Empty;
            }
            print(playerWantsToInteract);
        } 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!string.IsNullOrEmpty(textToShow))
            {
                tmpTextObject.text = textToShow;
            }
            else
            {
                tmpTextObject.text = string.Empty;
            }
            playerInTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!string.IsNullOrEmpty(tmpTextObject.text))
            {
                tmpTextObject.text = string.Empty;
            }
            playerWantsToInteract = false;
            playerInTrigger = false;
            print(playerWantsToInteract);
        }
    }
    public void ChangeText(string newText)
    {
        textToShow = newText;
    }
    public void ShowText()
    {
        tmpTextObject.text = textToShow;
    }
}
