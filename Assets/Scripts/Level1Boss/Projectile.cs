using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ParticleSystem destroyParticles;
    public float projectileSpeed = 20f;
    public Fuder fuder;

    private Vector2 targetPos;
    private Vector2 startingPos;
    private PlayerMovement player;
    private bool hit = false;
    private bool lateEnable = false;
    private Rigidbody2D rb;
    private bool canMove = false;
    private SpriteRenderer projectileSprite;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
    }
    private void Start()
    {
        startingPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        projectileSprite = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        targetPos = player.transform.position;
        lateEnable = true;
    }
    private void OnDisable()
    {
        destroyParticles.transform.position = transform.position;
        projectileSprite.enabled = false;
        destroyParticles.Play();
        hit = false;
    }
    private void Update()
    {
        if (lateEnable)
        {
            OnLateEnable();
        }
        if (Vector2.Distance(transform.position, targetPos) <= 0.1)
        {
            gameObject.SetActive(false);
            canMove = false;
        }
        else if (hit)
        {
            fuder.fuderAttacking = true;
            gameObject.SetActive(false);
            canMove = false;
        }
        else
        {
            canMove = true;
        }
    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            rb.MovePosition(Vector2.MoveTowards(transform.position, targetPos, projectileSpeed * Time.fixedDeltaTime));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hit = true;
            player.TakeCertainAmountOfHp();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            hit = true;
        }
    }
    private void OnLateEnable()
    {
        transform.position = startingPos;
        projectileSprite.enabled = true;
        lateEnable = false;
    }
}
