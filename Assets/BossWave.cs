using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWave : MonoBehaviour
{
    public float projectileSpeed;
    public bool goLeft;
    Rigidbody2D myRb;
    bool shouldFly = false;
    private void Awake()
    {
        myRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!shouldFly) return;
        if (goLeft)
        {
            myRb.velocity = Vector2.left * projectileSpeed;
        }
        else
        {
            myRb.velocity = Vector2.right * projectileSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            myRb.velocity = Vector2.zero;
            shouldFly = false;
        }
    }

    public void Fly()
    {
        shouldFly = true;
    }
}
