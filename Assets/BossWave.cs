using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWave : MonoBehaviour
{
    public float projectileSpeed;
    public bool goLeft;
    private Vector2 startPos;
    Rigidbody2D myRb;
    bool shouldFly = false;
    RugPullerController rugPull;
    private void Awake()
    {
        rugPull = FindObjectOfType<RugPullerController>();
        myRb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
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
            transform.position = new Vector2(rugPull.transform.position.x, transform.position.y);
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        shouldFly = true;
    }
}
