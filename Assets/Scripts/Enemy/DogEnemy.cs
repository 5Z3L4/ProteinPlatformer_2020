using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DogEnemy : MonoBehaviour
{
    public int moveSpeed;
    public int scoreValue;
    public float stopDistance = 1.5f;
    public float attackRange;
    public Transform attackPos;
    public float startTimeBtwAttack;
    public LayerMask whatIsEnemies;
    public Rigidbody2D mineRb;
    public bool facingRight = true;
    public bool shouldStartMoving = false;
    public Animator animator;

    private bool playerOnRight;
    private PlayerMovement player;
    private float timeBtwAttack = 0;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
    }
    private void Update()
    {
        if (CalculateXDistanceToPlayer() <= stopDistance)
        {
            Attack();
        }
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("KillBox"))
        {
            Die();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            timeBtwAttack -= Time.fixedDeltaTime;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            timeBtwAttack = 0;
        }
    }
    public void Attack()
    {
        if (player.hp > 0 && player.isGrounded)
        {
            Collider2D enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies).FirstOrDefault();
            if (timeBtwAttack <= 0)
            {
                timeBtwAttack = startTimeBtwAttack;
                if (enemiesToDamage != null)
                {
                    int tempSpeed = moveSpeed;
                    moveSpeed = 0;
                    animator.Play("Bite");
                    //dzwiek
                    player.TakeCertainAmountOfHp();
                    moveSpeed = tempSpeed;
                }
            }
        }
    }

    public void Move()
    {
        if (shouldStartMoving && CalculateXDistanceToPlayer() > stopDistance)
        {
            mineRb.velocity = new Vector2(moveSpeed * Time.fixedDeltaTime, mineRb.velocity.y);
            animator.SetBool("IsWalking", true);
        }
        else
        {
            mineRb.velocity = new Vector2(0, mineRb.velocity.y);
            animator.SetBool("IsWalking", false);
        }  
    }

    private void Die()
    {
        //dzwiek
        //animacja
        GameManager.Score += scoreValue;
        Destroy(gameObject);
    }
    
    public void Flip()
    {
        facingRight = !facingRight;
        moveSpeed *= -1;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private float CalculateXDistanceToPlayer()
    {
        return Mathf.Abs(transform.position.x - player.transform.position.x);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
