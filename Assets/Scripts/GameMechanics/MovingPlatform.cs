using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public enum Direction
    {
        UpAndDown,
        LeftAndRight
    }
    public Direction direction;
    public float speed;
    public float distance;
    private float startSpeed;
    private Vector2 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startSpeed = speed;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (direction == Direction.LeftAndRight)
        {
            if (transform.position.x > startPos.x + distance)
            {
                speed = -startSpeed;
            }
            if (transform.position.x < startPos.x - distance)
            {
                speed = startSpeed;
            }
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
        else if (direction == Direction.UpAndDown)
        {
            if (transform.position.y > startPos.y + distance)
            {
                speed = -startSpeed;
            }
            if (transform.position.y < startPos.y - distance)
            {
                speed = startSpeed;
            }
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
        }
        
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && transform.position.y < collision.gameObject.transform.position.y)
        {
            collision.transform.parent = transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.parent != null)
            {
                collision.transform.parent = null;
            }
        }
    }
}