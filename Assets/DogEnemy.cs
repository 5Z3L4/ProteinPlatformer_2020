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
    public float timeBtwAttack;
    public float attackRange;
    public Transform attackPos;
    public float startTimeBtwAttack;
    PlayerMovement player;
    public LayerMask whatIsEnemies;
    public Rigidbody2D mineRb;
    public bool facingRight = true;
    bool playerOnRight;
    public bool shouldStartMoving = false;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
    }
    private void Update()
    {
        Attack();
        CheckPlayerPos(player.transform.position);
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            shouldStartMoving = true;
            if (facingRight != playerOnRight)
            {
                Flip();
            }
        }
    }
    public void Attack()
    {
        if (CalculateXDistanceToPlayer() <= stopDistance)
        {
            if (player.hp > 0)
            {
                Collider2D enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies).FirstOrDefault();
                if (timeBtwAttack <= 0)
                {
                    timeBtwAttack = startTimeBtwAttack;
                    if (enemiesToDamage != null)
                    {
                        player.TakeCertainAmountOfHp();
                        Debug.Log(player.hp);
                    }
                }
                else
                {
                    timeBtwAttack -= Time.deltaTime;
                }
            }
        }
    }

    public void Move()
    {
        if (shouldStartMoving && CalculateXDistanceToPlayer() > stopDistance)
        {
            //if (transform.position.x - player.transform.position.x > 0)
            //{
                mineRb.velocity = new Vector2(moveSpeed * Time.fixedDeltaTime, mineRb.velocity.y);
            //}
            //else
            //{
            //    mineRb.velocity = new Vector2(1 * moveSpeed * Time.fixedDeltaTime, mineRb.velocity.y);
            //}
        }
        else
        {
            mineRb.velocity = new Vector2(0, mineRb.velocity.y);
        }
    }

    private IEnumerator WaitBeforeMove()
    {
        //dlugosc animacji
        yield return new WaitForSeconds(2f);
    }

    private void Die()
    {
        //dzwiek
        //animacja
        GameManager.Score += scoreValue;
        Destroy(this);
    }
    
    private void Flip()
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

    private void CheckPlayerPos(Vector2 playerPos)
    {  
        if ((playerPos.x - transform.position.x) >= 0)
        {
            playerOnRight = true;
        }
        else if ((playerPos.x - transform.position.x) < 0)
        {
            playerOnRight = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
