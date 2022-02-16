using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowResultsPanel : MonoBehaviour
{
    public GameObject resultsPanel;
    public GameObject canvas;

    public bool IsPlayerInTrigger = false;
    private void Update()
    {
        if (IsPlayerInTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                resultsPanel.SetActive(true);
                canvas.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsPlayerInTrigger = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsPlayerInTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsPlayerInTrigger = false;
        }
    }
}
