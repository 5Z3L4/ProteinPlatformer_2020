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
        if ((collision.gameObject.layer == 3 || collision.gameObject.layer == 16) && player.isAirborn && player.isGrounded)
        {
            player.PlayParticleSystem(player.jumpAndLand);
            player.isAirborn = false;
        }
    }
}
