using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDamage : MonoBehaviour
{

    private PlayerMovement player;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.hp--;
            print(player.hp);
        }

    }
}
