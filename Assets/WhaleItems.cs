using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleItems : MonoBehaviour
{
    PlayerMovement _player;
    private void Awake()
    {
        _player = _player = FindObjectOfType<PlayerMovement>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player.TakeCertainAmountOfHp();
            Destroy(gameObject);
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
