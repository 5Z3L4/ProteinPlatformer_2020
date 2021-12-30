using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kark : MonoBehaviour //,Enemy
{
    PlayerMovement player;
    [SerializeField] float timeBtwAttack;
    [SerializeField] float startTimeBtwAttack;
    [SerializeField] Transform castPos;
    [SerializeField] float baseCastDist;
    bool facingRight;
    Rigidbody2D rb;
    [SerializeField] float moveSpeed;
    Vector3 baseScale;
    bool canMove;
    bool canAttack;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        timeBtwAttack = startTimeBtwAttack;
        canAttack = false;
        canMove = true;
        baseScale = transform.localScale;
        facingRight = true;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (canAttack)
        {
            Attack();
        }
    }
    private void FixedUpdate()
    {
        float vX = moveSpeed;
        if (!facingRight)
        {
            vX = -moveSpeed;
        }
        if (canMove)
        {
            rb.velocity = new Vector2(vX, rb.velocity.y);
        }
        
        if (IsHittingWall() || IsNearEdge())
        {
            if (!facingRight)
            {
                Flip(true);
            }
            else
            {
                Flip(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (facingRight == player.facingRight && player.isSliding)
            {
                Destroy(gameObject);
            }
            else if (facingRight != player.facingRight)
            {
                canMove = false;
                canAttack = true;
            }
        } 
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canMove = true;
            canAttack = false;
            timeBtwAttack = startTimeBtwAttack;
        }
    }
    private void Attack()
    {
        if (timeBtwAttack <= 0)
        {
            player.hp--;
            print("Player hp: " + player.hp);
            //anim.Play("");
            Flip(!facingRight);
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }
    bool IsHittingWall()
    {
        bool val = false;
        float castDist = baseCastDist;
        if (!facingRight)
        {
            castDist = -baseCastDist;
        }
        Vector3 targetPos = castPos.position;
        targetPos.x += castDist;
        Debug.DrawLine(castPos.position, targetPos, Color.blue);
        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = true;
        }
        else
        {
            val = false;
        }
        return val;
    }
    void Flip(bool newDirection)
    {
        Vector3 newScale = baseScale;

        if (!newDirection)
        {
            newScale.x = -baseScale.x;
        }
        else
        {
            newScale.x = baseScale.x;
        }

        transform.localScale = newScale;
        facingRight = newDirection;
    }
    bool IsNearEdge()
    {
        bool val = true;
        float castDist = baseCastDist;
        Vector3 targetPos = castPos.position;
        targetPos.y -= castDist;
        Debug.DrawLine(castPos.position, targetPos, Color.red);
        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = false;
        }
        else
        {
            val = true;
        }
        return val;
    }
}