using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatCoinerController : MonoBehaviour
{
    public bool FacingRight;
    public int hp = 5;
    public bool CanGetHit = false;
    public bool CanHitPlayer = false;
    public GameObject SliderObj;
    public GameObject Ticket;
    public Slider HpSlider;
    private PlayerMovement _player;
    private Animator _anim;
    private Rigidbody2D _rb;
    private bool _runAwayFromPlayer = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<PlayerMovement>();
        _anim = GetComponent<Animator>();
        HpSlider.maxValue = hp;
    }
    private void Start()
    {
        HpSlider.value = hp;
    }
    void Update()
    {
        if (hp <= 0)
        {
            gameObject.layer = 13;
            Ticket.SetActive(true);
            return;
        }

        if (CanHitPlayer)
        {
            gameObject.layer = 0;
            _rb.isKinematic = true;
        }
        if (!_runAwayFromPlayer)
        {
            if (transform.position.x > _player.transform.position.x)
            {
                if (FacingRight) return;
                Flip();
                FacingRight = true;
            }
            if (transform.position.x < _player.transform.position.x)
            {
                if (!FacingRight) return;
                Flip();
                FacingRight = false;
            }
        }
        else
        {
            if (transform.position.x < _player.transform.position.x)
            {
                if (FacingRight) return;
                Flip();
                FacingRight = true;
            }
            if (transform.position.x > _player.transform.position.x)
            {
                if (!FacingRight) return;
                Flip();
                FacingRight = false;
            }
        }
       
    }

    internal void RunAway()
    {
        _runAwayFromPlayer = !_runAwayFromPlayer;
    }

    public void Flip()
    {
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            BallHit();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (CanGetHit && _player.isCharging)
            {
                BallHit();
                _rb.isKinematic = false;
                gameObject.layer = 13;
                _anim.SetBool("Idle", true);
                CanGetHit = false;
                CanHitPlayer = false;
                _anim.Play("BeatCoinerGetHit");
                _rb.AddForce(new Vector2(5f, 1f));
            }
        }
    }
    void BallHit()
    {
        hp--;
        HpSlider.value = hp;
        StartCoroutine(DestroyAllBalls());
        if (hp <= 5)
        {
            _anim.SetBool("2ndPhase", true);
        }
        if (hp<=0)
        {
            SliderObj.SetActive(false);
            _anim.SetBool("IsDead", true);
            _rb.gravityScale = 100;
        }
        if (!_anim.GetBool("BallsDidBoom"))
        {
            _anim.SetBool("BallsDidBoom", true);
        }
    }

    IEnumerator DestroyAllBalls()
    {
        yield return new WaitForSeconds(0.2f);
        var allBalls = FindObjectsOfType<Ball>();
        foreach (var ball in allBalls)
        {
            ball.DestroyBall();
        }
    }
}
