using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatCoinerBagHit : MonoBehaviour
{
    private PlayerMovement _player;
    private BeatCoinerController _bc;
    private void Start()
    {
        _player = FindObjectOfType<PlayerMovement>();
        _bc = FindObjectOfType<BeatCoinerController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player.TakeCertainAmountOfHp();
        }
    }
}
