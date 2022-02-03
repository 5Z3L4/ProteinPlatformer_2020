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
            RaycastHit2D hitGround = Physics2D.Linecast(dog.transform.position, player.transform.position, 1 << LayerMask.NameToLayer("Ground"));
            if (hitGround.collider == null)
            {
                RaycastHit2D hitPlayer = Physics2D.Linecast(dog.transform.position, player.transform.position, 1 << LayerMask.NameToLayer("Player"));
                if (hitPlayer.collider != null)
                {
                    if (hitPlayer.collider.gameObject.CompareTag("Player"))
                    {
                        dog.shouldStartMoving = true;
                        if (dog.facingRight != playerOnRight)
                        {
                            dog.Flip();
                        }
                    }
                }
            }
            else
            {
                dog.shouldStartMoving = false;
            }
            Debug.DrawLine(dog.transform.position, player.transform.position);
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
