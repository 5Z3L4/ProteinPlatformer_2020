using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadGuyBackColl : MonoBehaviour
{
    public Kark BadGuy;
    private PlayerMovement _player;

    private void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if ((BadGuy.facingRight == _player.facingRight && _player.isSliding) || _player.isCharging)
            {
                StartCoroutine(BadGuy.Die());
            }
        }
        
    }
}
