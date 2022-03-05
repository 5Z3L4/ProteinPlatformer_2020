using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flip : MonoBehaviour
{
    public bool isFacingRight;

    private bool playerOnRight;
    private PlayerMovement player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
    }
    void Update()
    {
        CheckPlayerPos(player.transform.position);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if ((playerOnRight && !isFacingRight) || (!playerOnRight && isFacingRight))
            {
                Flip();
            }
        }
    }

    private void CheckPlayerPos(Vector2 playerPos)
    {
        if ((playerPos.x - transform.position.x) >= 0)
        {
            playerOnRight = true;
        }
        else if ((playerPos.x - transform.position.x) < 0)
        {
            playerOnRight = false;
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
