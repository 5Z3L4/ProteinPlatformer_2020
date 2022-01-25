using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowText : MonoBehaviour
{
    public string textToShow;
    private PlayerBubble playerTextHolder;
    void Start()
    {
        playerTextHolder = GameObject.Find("PlayerBubble").GetComponent<PlayerBubble>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerTextHolder.gameObject.SetActive(true);
            playerTextHolder.SetSizeOfTextBackground(textToShow);
        }
    }

}
