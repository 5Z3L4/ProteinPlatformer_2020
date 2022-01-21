using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RugPullerController : MonoBehaviour
{
    PlayerMovement player;
    public bool facingRight = false;
    public bool facingLeft = true;
    bool jumped = false;
    bool isGrounded;
    public BossWave left;
    public BossWave right;
    public GameObject leftObj;
    public GameObject rightObj;
    Rigidbody2D myRb;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public ScreenShake sc;
    private void Awake()
    {
        myRb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
    } 
    public void ShockWave()
    {
        leftObj.SetActive(true);
        rightObj.SetActive(true);
        sc.Shakecamera(5f, .1f);
    }
    // Update is called once per frame
    void Update()
    {
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
    }
}
