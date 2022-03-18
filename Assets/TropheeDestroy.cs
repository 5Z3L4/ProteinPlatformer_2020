using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TropheeDestroy : MonoBehaviour
{
    public ParticleSystem destroyParticle;

    private SpriteRenderer _sprite;
    private float _timer = 0.25f;
    private bool _isTouchingPlayer = false;
    private Rigidbody2D _rb;
    private float _rbVel;
    private bool _tropheeAlive = true;
    private BoxCollider2D _collider;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        _rbVel = _rb.velocity.magnitude;
    }
    // Update is called once per frame
    void Update()
    {
        if (_isTouchingPlayer && _timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        if (_timer <= 0 && _tropheeAlive)
        {
            _tropheeAlive = false;
 
            StartCoroutine(DestroyTrophee());
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            _isTouchingPlayer = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            if (_rbVel > 5 || _rbVel < -5)
            {
                StartCoroutine(DestroyTrophee());
            }
        }
        else if (collision.collider.CompareTag("KillBox"))
        {
            StartCoroutine(DestroyTrophee());
        }
        else if (collision.collider.CompareTag("Enemy"))
        {
            StartCoroutine(DestroyTrophee());
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            _isTouchingPlayer = false;
        }
    }
    private IEnumerator DestroyTrophee()
    {
        SFXManager.PlaySound(SFXManager.Sound.ChaliceBreak, transform.position);
        _sprite.enabled = false;
        _collider.enabled = false;
        destroyParticle.Play();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
