using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleItems : MonoBehaviour
{
    public bool isDog = false;

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
            if (isDog)
            {
                SFXManager.PlaySound(SFXManager.Sound.Bark, transform.position);
            }
            else
            {
                SFXManager.PlaySound(SFXManager.Sound.MetalHit, transform.position);
            }
            Destroy(gameObject);
        }
    }

    public void DestroyObject()
    {
        if (isDog)
        {
            SFXManager.PlaySound(SFXManager.Sound.Bark, transform.position);
        }
        else
        {
            SFXManager.PlaySound(SFXManager.Sound.MetalHit, transform.position);
        }
        Destroy(gameObject);
    }
}
