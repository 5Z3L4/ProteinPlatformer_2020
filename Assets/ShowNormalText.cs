using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowNormalText : MonoBehaviour
{
    public TMP_Text tmpTextObject;
    public string textToShow;
    public bool shouldDisplayText = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (shouldDisplayText)
            {
                if (!string.IsNullOrEmpty(textToShow))
                {
                    ShowText();
                }
                else
                {
                    HideText();
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!string.IsNullOrEmpty(tmpTextObject.text))
            {
                HideText();
            }
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
    public void HideText()
    {
        tmpTextObject.text = string.Empty;
    }
}
