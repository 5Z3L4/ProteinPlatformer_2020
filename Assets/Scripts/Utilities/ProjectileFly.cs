using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFly : MonoBehaviour
{
    public float ProjectileSpeed = -250f;
    private float _projectileSpeed;
    public LayerMask WhatIsGround;
    private PlayerMovement _player;
    private Rigidbody2D _myRb;
    void Awake()
    {
        _player = FindObjectOfType<PlayerMovement>();
        _myRb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ChoseShotingDirection();
    }
    // Update is called once per frame
    void Update()
    {
        _myRb.velocity = new Vector2(_projectileSpeed * Time.deltaTime, 0);
    }

    void ChoseShotingDirection()
    {
        if (_player.transform.position.x - transform.position.x < 0)
        {
            _projectileSpeed = ProjectileSpeed;
        }
        else
        {
            _projectileSpeed = -ProjectileSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _player.TakeCertainAmountOfHp();
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == WhatIsGround)
        {
            Destroy(gameObject);
        }
    }
}
