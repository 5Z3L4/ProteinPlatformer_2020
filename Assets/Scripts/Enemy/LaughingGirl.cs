using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaughingGirl : MonoBehaviour //Enemy
{
    public TextBubble bubble;
    public bool isFacingRight;
    public bool shouldAttack = true;
    public Transform minePosition;
    public GameObject player;
    [SerializeField] ParticleSystem[] icons;
    [SerializeField] private Animation anim;
    [SerializeField] private float timeBtwAttack;
    [SerializeField] private float startTimeBtwAttack;


    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    private void Start()
    {
        isFacingRight = true;
        minePosition = GetComponent<Transform>();
        anim = GetComponent<Animation>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (icons.Length > 0)
            {
                icons[0].Play();
                icons[1].Play();
            }
            if ((!bubble.shouldDisplayNegativeTexts && isFacingRight == player.GetComponent<PlayerMovement>().facingRight) || (bubble.shouldDisplayNegativeTexts && isFacingRight != player.GetComponent<PlayerMovement>().facingRight))
            {
                Flip();
            }
        }  
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (shouldAttack)
            {
                ShootWave();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (shouldAttack)
            {
                timeBtwAttack = startTimeBtwAttack;
            }
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
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
