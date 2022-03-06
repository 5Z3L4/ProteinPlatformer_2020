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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player.TakeCertainAmountOfHp();
        }
    }
}
