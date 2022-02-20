using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckButtonPress : MonoBehaviour
{
    public GameObject objToSetActive;
    private bool isPlayerInTrigger = false;
    private void Update()
    {
        if (!isPlayerInTrigger) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            objToSetActive.SetActive(true);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }
}
