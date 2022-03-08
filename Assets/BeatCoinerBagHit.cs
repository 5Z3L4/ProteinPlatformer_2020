using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatCoinerBagHit : MonoBehaviour
{
    private PlayerMovement _player;
    private void Start()
    {
        _player = FindObjectOfType<PlayerMovement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player.TakeCertainAmountOfHp();
        }
    }
}
