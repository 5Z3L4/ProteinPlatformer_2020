using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Sprite ballWithShiba;

    private ParticleSystem _particleSystem;
    private SpriteRenderer _spriteRenderer;
    private bool _isShibaInside = false;
    private void Awake()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("KillBox") || collision.gameObject.CompareTag("Enemy"))
        {
            DestroyBall();
        }
        if (collision.gameObject.CompareTag("DogEnemy"))
        {
            Destroy(collision.gameObject);
            SFXManager.PlaySound(SFXManager.Sound.ShibaPullInBall, transform.position);
            _spriteRenderer.sprite = ballWithShiba;
            _isShibaInside = true;
        }
    }

    public void DestroyBall()
    {
        SFXManager.PlaySound(SFXManager.Sound.BalloonBlowUp, transform.position);
        if (_isShibaInside)
        {
            SFXManager.PlaySound(SFXManager.Sound.ShibaDeath, transform.position);
        }
        GetComponent<SpriteRenderer>().enabled = false;
        _particleSystem.Play();
        Destroy(gameObject, 0.15f);
    }
}
