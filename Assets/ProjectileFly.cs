using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFly : MonoBehaviour
{
    public float ProjectileSpeed = -250f;
    public LayerMask WhatIsGround;
    private PlayerMovement _player;
    private Rigidbody2D _myRb;
    void Awake()
    {
        _player = FindObjectOfType<PlayerMovement>();
        _myRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _myRb.velocity = new Vector2(ProjectileSpeed * Time.deltaTime, 0);
    }
}
