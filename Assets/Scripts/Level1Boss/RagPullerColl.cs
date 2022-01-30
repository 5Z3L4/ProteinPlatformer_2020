using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagPullerColl : MonoBehaviour
{
    PlayerMovement player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.TakeCertainAmountOfHp();
        }
    }
}
