using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBubble : MonoBehaviour
{
    [SerializeField] private GameObject NPCBubble;
    [SerializeField]  private TextBubble textToShow;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        NPCBubble.SetActive(true);
        textToShow.BubbleSetup();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        NPCBubble.SetActive(false);
    }
}
