using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowResultsPanel : MonoBehaviour
{
    public GameObject resultsPanel;
    public GameObject canvas;

    public bool IsPlayerInTrigger = false;
    [SerializeField]
    private bool _shouldPlayerClick = true;
    private void Update()
    {
        if (!_shouldPlayerClick) return;
        if (IsPlayerInTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ShowPanel();
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

    public void ShowPanel()
    {
        resultsPanel.SetActive(true);
        canvas.SetActive(false);
    }
}
