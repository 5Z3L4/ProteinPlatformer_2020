using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatCoinerController : MonoBehaviour
{
    private bool _facingRight;
    public int hp = 5;
    public GameObject SliderObj;
    public Slider HpSlider;
    private PlayerMovement _player;
    private Animator _anim;
    private bool _runAwayFromPlayer = false;

    private void Awake()
    {
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
        if (!_runAwayFromPlayer)
        {
            if (transform.position.x > _player.transform.position.x)
            {
                if (_facingRight) return;
                Flip();
                _facingRight = true;
            }
            if (transform.position.x < _player.transform.position.x)
            {
                if (!_facingRight) return;
                Flip();
                _facingRight = false;
            }
        }
        else
        {
            if (transform.position.x < _player.transform.position.x)
            {
                if (_facingRight) return;
                Flip();
                _facingRight = true;
            }
            if (transform.position.x > _player.transform.position.x)
            {
                if (!_facingRight) return;
                Flip();
                _facingRight = false;
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
            Destroy(gameObject);
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
