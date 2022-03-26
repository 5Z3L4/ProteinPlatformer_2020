using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkController : MonoBehaviour
{
    public ParticleSystem Explosion;
    public ParticleSystem _explosionPlayer;
    private PlayerMovement _player;
    private void Awake()
    {
        _player = FindObjectOfType<PlayerMovement>();
        _explosionPlayer = GameObject.Find("ExplosionPlayer").GetComponent<ParticleSystem>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player.TakeCertainAmountOfHp();
            _explosionPlayer.Play();
            Destroy(gameObject);
            SFXManager.PlaySound(SFXManager.Sound.FireworkExplosion, transform.position);
        }
        else if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("DogEnemy"))
        {
            Explode();
        }
    }

    public void Explode()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Explosion.Play();
        SFXManager.PlaySound(SFXManager.Sound.FireworkExplosion, transform.position);
    }
}
