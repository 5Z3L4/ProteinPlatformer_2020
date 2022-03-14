using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSmashOff : MonoBehaviour
{
    private PlayerMovement _player;
    private void Awake()
    {
        _player = FindObjectOfType<PlayerMovement>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Smashable") && collision.gameObject.layer == 3)
        {
            _player.isSmashing = false;
            _player.SmashOver();
            gameObject.SetActive(false);
        }
    }
}
