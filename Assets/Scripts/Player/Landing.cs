using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landing : MonoBehaviour
{
    public PlayerMovement player;
    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") && player.isAirborn && player.isGrounded)
        {
            player.PlayParticleSystem(player.jumpAndLand);
            player.isAirborn = false;
        }
    }
}
