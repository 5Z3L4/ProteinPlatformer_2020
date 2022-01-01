using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaughingGirl : MonoBehaviour //Enemy
{
    [SerializeField] private Animation anim;
    [SerializeField] private float timeBtwAttack;
    [SerializeField] private float startTimeBtwAttack;
    [HideInInspector]
    public bool isFacingRight;

    public Transform minePosition;
    public GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    private void Start()
    {
        isFacingRight = false;
        Flip();
        minePosition = GetComponent<Transform>();
        anim = GetComponent<Animation>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isFacingRight == player.GetComponent<PlayerMovement>().facingRight)
        {
            Flip();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        ShootWave();
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
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
