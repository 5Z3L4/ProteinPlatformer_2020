using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBubbleTrigger : MonoBehaviour
{
    private PlayerBubble playerBubble;
    public DialogueObject newPlayerBubbleText;
    private void Awake()
    {
        playerBubble = GameObject.Find("PlayerBubble").GetComponent<PlayerBubble>();
    }
    private void Start()
    {
        playerBubble.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerBubble.gameObject.SetActive(true);
            playerBubble.BubbleSetup(newPlayerBubbleText);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerBubble.gameObject.SetActive(false);
        }
    }
}
