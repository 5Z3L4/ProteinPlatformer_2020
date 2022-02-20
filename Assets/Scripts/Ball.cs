using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private void Awake()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("KillBox") || collision.gameObject.CompareTag("DogEnemy"))
        {
            DestroyBall();
        }
    }

    public void DestroyBall()
    {
        SFXManager.PlaySound(SFXManager.Sound.BalloonBlowUp, transform.position);
        GetComponent<SpriteRenderer>().enabled = false;
        particleSystem.Play();
        Destroy(gameObject, 0.15f);
    }
}
