using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public abstract int EnemyHealth { get; set; }

    public abstract int enemyDamage { get; set; }

    public abstract bool isLookingAtPlayer { get; set; }

    public bool isChasing { get; set; }

    public float timeBtwAttack { get; set; }

    public LayerMask whatIsEnemies { get; set; }

    public float startTimeBtwAttack { get; set; }



    public Transform minePosition;
    public Rigidbody2D mineRb;
    private void Start()
    {
        minePosition = GetComponent<Transform>();
        mineRb = GetComponent<Rigidbody2D>();
    }
    public abstract void ChasePlayer();
    public abstract void LookForPlayer();
    public void Die()
    {
        Destroy(this.gameObject);
    }
}
