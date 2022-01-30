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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && player.isAirborn)
        {
            Invoke("PlayParticle", 0.02f);
            
            player.isAirborn = false;
        }
    }
    private void PlayParticle()
    {
        player.PlayParticleSystem(player.jumpAndLand);
    }
}
