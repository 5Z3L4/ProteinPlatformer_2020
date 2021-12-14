using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaughingGirl : MonoBehaviour //Enemy
{
    public GameObject projectileRef;
    public float focusDistance;
    public float timeBtwAttack;
    public float startTimeBtwAttack;
    public float distanceToPlayer;
    public Transform shootPos;
    [HideInInspector]
    public int isFacingRight;

    public Transform minePosition;
    public Transform playerPos;
    

    private void Start()
    {
        isFacingRight = 1;
        minePosition = GetComponent<Transform>();
        shootPos = GameObject.Find("ShootPos").transform;
    }

    private void Update()
    {
        playerPos = GameObject.Find("Player").transform;

        distanceToPlayer = Vector2.Distance(minePosition.position, playerPos.position);

        if (distanceToPlayer <= focusDistance)
        {
            
            if (playerPos.position.x > transform.position.x && transform.localScale.x < 0 || playerPos.position.x < transform.position.x && transform.localScale.x > 0)
            {
                isFacingRight *= -1;
                Vector3 scaler = transform.localScale;
                scaler.x *= -1;
                transform.localScale = scaler;
            }
            ShootProjectile();
        }
    }
    
    public void ShootProjectile()
    {
        if (timeBtwAttack <= 0)
        {
            timeBtwAttack = startTimeBtwAttack;
            Instantiate(projectileRef, shootPos.position, Quaternion.identity);
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }
}
