using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kark : MonoBehaviour //,Enemy
{
    public Transform groundDetection;
    public float stopDistance;
    public bool isDead;
    public float speed;
    public bool movingRight;
    public bool canAttack;
    private PlayerMovement player;
    public float timeBtwAttack;
    public float startTimeBtwAttack;
    private bool mustTurn;
    public LayerMask groundLayer;



    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        movingRight = true;
        isDead = false;
        canAttack = false;
    }
    void Update()
    {
        if (canAttack)
        {
            Attack();
        }
        if (!isDead && !canAttack)
        {
            Move();
        }
    }
    private void FixedUpdate()
    {
        mustTurn = !Physics2D.OverlapCircle(groundDetection.position, 0.1f, groundLayer);
    }
    public void Move()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2f);
        if (!groundInfo.collider)
        {
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player.facingRight == movingRight && player.isSliding)
            {
                isDead = true;
                Destroy(gameObject);
            }
            else if(player.facingRight == movingRight && !player.isSliding)
            {
                Flip();
                canAttack = true;
            }
            else
            {
                canAttack = true;
            }
        }
    }
    private void Attack()
    {
        if (timeBtwAttack <= 0)
        {
            print("Kark is attacking");
            player.hp--;
            print("Player hp: " + player.hp);
            //anim.Play("");
            timeBtwAttack = startTimeBtwAttack;
            canAttack = false;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }
    private void Flip()
    {
        print("Kark is flipping");
        movingRight = !movingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
