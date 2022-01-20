using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFalling : MonoBehaviour
{
    Rigidbody2D myRb;
    Vector3 startPos;
    SpriteRenderer mySprite;
    BoxCollider2D myCollider;
    public BoxCollider2D myColliderTrigger;
    [SerializeField] private ParticleSystem platformDestroy;
    [SerializeField] private ParticleSystem platformRespawn;

    private void Awake()
    {
        myRb = GetComponent<Rigidbody2D>();
        mySprite = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        startPos = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            HidePlatform();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke("DropPlatform", 0.5f);
        }
    }
    private void DropPlatform()
    {
        myRb.isKinematic = false;
    }

    private void HidePlatform()
    {
        myColliderTrigger.enabled = false;
        myCollider.enabled = false;
        mySprite.enabled = false;
        myCollider.enabled = false;
        myRb.velocity = Vector2.zero;
        myRb.isKinematic = true;
        platformDestroy.Play();
        Invoke("PlayRespawnParticle", 9f);
        Invoke("RespawnPlatform", 10);
    }

    private void RespawnPlatform()
    {
        transform.position = startPos;
        mySprite.enabled = true;
        myCollider.enabled = true;
        myColliderTrigger.enabled = true;
    }

    void PlayRespawnParticle()
    {
        platformRespawn.transform.position = startPos;
        platformRespawn.Play();
    }

}
