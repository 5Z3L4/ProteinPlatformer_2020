using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    private bool isPlayerInTrigger;
    public GameObject ObjectToSetActive;
    // Update is called once per frame
    void Update()
    {
        if (!isPlayerInTrigger) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (ObjectToSetActive != null)
            {
                ObjectToSetActive.SetActive(true);
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }
}
