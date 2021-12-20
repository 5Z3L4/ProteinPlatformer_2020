using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaughingGirl : MonoBehaviour //Enemy
{
    [SerializeField] private Animation anim;
    [SerializeField] private float focusDistance;
    [SerializeField] private float timeBtwAttack;
    [SerializeField] private float startTimeBtwAttack;
    [SerializeField] private float distanceToPlayer;
    [HideInInspector]
    public int isFacingRight;

    public Transform minePosition;
    public Transform playerPos;
    

    private void Start()
    {
        isFacingRight = 1;
        minePosition = GetComponent<Transform>();
        anim = GetComponent<Animation>();
    }

    private void Update()
    {
        playerPos = GameObject.Find("Player").transform;

        distanceToPlayer = Vector2.Distance(minePosition.position, playerPos.position);

        if (distanceToPlayer <= focusDistance)
        {
            if (playerPos.position.x > transform.position.x && transform.localScale.x < 0 || playerPos.position.x < transform.position.x && transform.localScale.x > 0)
            {
                Invoke("Flip", 2f);
            }
            ShootWave();
        }
    }
    
    public void ShootWave()
    {
        if (timeBtwAttack <= 0)
        {
            timeBtwAttack = startTimeBtwAttack;
            anim.Play("laughing_girl_wave");
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }
    private void Flip()
    {
        isFacingRight *= -1;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
