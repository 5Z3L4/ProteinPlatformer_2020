using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RugPullerController : MonoBehaviour
{
    PlayerMovement player;
    public bool facingRight = false;
    public bool facingLeft = true;
    bool jumped = false;
    bool isGrounded;
    bool shouldFlip;
    public Slider hpSlider;
    public BossWave left;
    public BossWave right;
    public BossWave left2;
    public BossWave right2;
    public GameObject leftObj;
    public GameObject rightObj;
    public GameObject leftObj2;
    public GameObject rightObj2;
    int hp = 15;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public ScreenShake sc;
    public GameObject projectile;
    public AudioSource bgMusic;
    public AudioClip victoryMusic;
    public BoxCollider2D coll;
    public Rigidbody2D rb;
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerMovement>();
    } 
    public void ShockWave()
    {
        if (leftObj.activeSelf)
        {
            leftObj2.SetActive(true);
        }
        else
        {
            leftObj.SetActive(true);
        }
        if (rightObj.activeSelf)
        {
            rightObj2.SetActive(true);
        }
        else
        {
            rightObj.SetActive(true);
        }
        SFXManager.PlaySound(SFXManager.Sound.BossBoom, transform.position);
        sc.Shakecamera(5f, .1f);
    }
    // Update is called once per frame
    void Update()
    {
        hpSlider.value = hp;
        if (hp <= 8)
        {
            anim.SetBool("Second", true);
        }
        if (hp <= 0)
        {
            if (coll.isTrigger) return;

            if (!anim.GetBool("BossDied"))
            {
                bgMusic.clip = victoryMusic;
                bgMusic.Play();
                bgMusic.loop = false;
                anim.SetBool("BossDied", true);
            }
                
            if (anim.GetBool("BossFlee"))
            {
                Flip();
                coll.isTrigger = true;
                rb.isKinematic = true;
            }
            return;
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.3f, whatIsGround);
        if (!isGrounded)
        {
            jumped = true;
        }
        if (isGrounded && jumped)
        {
            ShockWave();
            jumped = false;
        }

        if (transform.position.x > player.transform.position.x)
        {
            if (facingRight) return;
            Flip();
            facingRight = true;
            facingLeft = false;
        }
        if (transform.position.x < player.transform.position.x)
        {
            if (facingLeft) return;
            Flip();
            facingLeft = true;
            facingRight = false;
        } 
    }
    public void Flip()
    {
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
        left.goLeft = !left.goLeft;
        right.goLeft = !right.goLeft;
        left2.goLeft = !left2.goLeft;
        right2.goLeft = !right2.goLeft;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            hp--;
            SFXManager.PlaySound(SFXManager.Sound.Punch, transform.position);
            Destroy(collision.gameObject);
        }
    }
}
