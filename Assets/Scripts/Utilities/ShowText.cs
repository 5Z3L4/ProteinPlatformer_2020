using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ShowText : MonoBehaviour
{
    public string textToShow;
    public PlayerBubble playerTextHolder;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerTextHolder.gameObject.SetActive(true);
            playerTextHolder.SetSizeOfTextBackground(textToShow);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerTextHolder.gameObject.SetActive(false);
        }
    }
}
