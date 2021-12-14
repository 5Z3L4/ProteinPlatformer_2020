using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaughingGirl : MonoBehaviour //Enemy
{
    public GameObject projectileRef;
    public float focusDistance;
    public float timeBtwAttack;
    public float startTimeBtwAttack;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public float playerPos;

    public Transform minePosition;
    public Rigidbody2D mineRb;
    

    private void Start()
    {
        focusDistance = 7f;
        timeBtwAttack = 2f;
        startTimeBtwAttack = 4f;
        minePosition = GetComponent<Transform>();
        mineRb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        playerPos = GameObject.Find("Player").transform.position.x;
        if (SpottedPlayer())
        {
            ShootProjectile();
        }
        
    }

    public float CalculateXDistanceToPlayer()
    {
        return Mathf.Abs(minePosition.transform.position.x - playerPos);
    }

    public bool SpottedPlayer()
    {
        if (CalculateXDistanceToPlayer() <= focusDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public void ShootProjectile()
    {
        if (timeBtwAttack <= 0)
        {
            timeBtwAttack = startTimeBtwAttack;
            Instantiate(projectileRef, transform.position, Quaternion.identity);
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
        
    }
}
