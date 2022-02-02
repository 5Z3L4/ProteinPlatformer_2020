using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogDetectingPlayer : MonoBehaviour
{
    public bool playerOnRight;

    private PlayerMovement player;
    private DogEnemy dog;
    private void Awake()
    {
        dog = GetComponentInParent<DogEnemy>();
        player = FindObjectOfType<PlayerMovement>();
    }
    private void Update()
    {
        CheckPlayerPos(player.transform.position);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dog.shouldStartMoving = true;
            if (dog.facingRight != playerOnRight)
            {
                dog.Flip();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dog.shouldStartMoving = false;
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
}
