using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWave : MonoBehaviour
{
    public float projectileSpeed;
    public bool goLeft;
    Rigidbody2D myRb;
    bool shouldFly = false;
    RugPullerController rugPull;
    float direction;
    public Transform startPos;
    PlayerMovement player;
    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
        rugPull = FindObjectOfType<RugPullerController>();
        myRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!shouldFly) return;
        myRb.velocity = new Vector2(direction * projectileSpeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            myRb.velocity = Vector2.zero;
            shouldFly = false;
            transform.position = new Vector2(rugPull.transform.position.x, -56.25f);
            gameObject.SetActive(false);
        }
        if (collision.CompareTag("Player"))
        {
            player.TakeCertainAmountOfHp();
        }
    }

    private void OnEnable()
    {
        transform.position = new Vector2(startPos.transform.position.x, -57.25f);
        if (goLeft)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
        shouldFly = true;
    }
}
