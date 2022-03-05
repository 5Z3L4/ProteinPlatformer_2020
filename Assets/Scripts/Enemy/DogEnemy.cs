using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DogEnemy : MonoBehaviour
{
    public int moveSpeed;
    public int scoreValue;
    public float stopDistance;
    public float attackRange;
    public Transform attackPos;
    public float startTimeBtwAttack;
    public LayerMask whatIsEnemies;
    public Rigidbody2D mineRb;
    public bool facingRight = true;
    public bool shouldStartMoving = false;
    public Animator animator;
    private PlayerMovement player;
    private float timeBtwAttack = 0;
    private float _startBarkTime = 1f;
    private float _barkTime = 0f;
    private void Start()
    {
        stopDistance = attackRange + (attackRange/2);
    }

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
        if (player.hp > 0)
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
        if (shouldStartMoving && CalculateXDistanceToPlayer() > stopDistance && moveSpeed != 0)
        {
            Bark();
            mineRb.velocity = new Vector2(moveSpeed * Time.fixedDeltaTime, mineRb.velocity.y);
            animator.SetBool("IsWalking", true);
            timeBtwAttack = 0;
        }
        else
        {
            mineRb.velocity = new Vector2(0, mineRb.velocity.y);
            animator.SetBool("IsWalking", false);
        }  
    }

    private void Bark()
    {
        if (_barkTime <= 0)
        {
            SFXManager.PlaySound(SFXManager.Sound.Bark, transform.position);
            _barkTime = _startBarkTime;
        }
        else
        {
            _startBarkTime -= Time.deltaTime;
        }
        
    }

    private void Die()
    {
        SFXManager.PlaySound(SFXManager.Sound.ShibaDeath, transform.position);
        animator.Play("Die");
        GameManager.Score += scoreValue;
        Destroy(gameObject, 0.5f);
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
        return Mathf.Abs(attackPos.transform.position.x - player.transform.position.x);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
