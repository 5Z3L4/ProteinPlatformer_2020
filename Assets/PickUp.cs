using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Collect collect;
    public CollectItem collectItem;
    private bool playerInTrigger;

    private void Awake()
    {
        collect = GetComponent<Collect>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                collect.CollectItem();
                GameManager.collectedSpecificItems++;
                GetComponentInChildren<ParticleSystem>().Play();
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }
}
