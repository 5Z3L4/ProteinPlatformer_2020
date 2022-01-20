using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RugPullerController : MonoBehaviour
{
    PlayerMovement player;
    public bool facingRight = false;
    public bool facingLeft = true;
    bool doShockwave = false;
    public BossWave left;
    public BossWave right;
    Rigidbody2D myRb;
    private void Awake()
    {
        myRb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
    } 
    public void ShockWave()
    {
        left.Fly();
        right.Fly();
    }
    // Update is called once per frame
    void Update()
    {
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
        if (myRb.velocity.y < 0)
        {
            doShockwave = true;
        }
        if (doShockwave)
        {
            if (myRb.velocity.y >=0)
            {
                ShockWave();
            }  
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
