using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowNormalText : MonoBehaviour
{
    public TMP_Text TextObject;
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
            if (!string.IsNullOrEmpty(TextObject.text))
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
        TextObject.text = textToShow;
    }
    public void HideText()
    {
        TextObject.text = string.Empty;
    }
}
