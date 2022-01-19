using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    BoxCollider2D myColl;
    public Camera mainCamera;
    public int numberOfBackgrounds;

    void Awake()
    {
        myColl = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        float distance = Mathf.Abs(transform.position.x - mainCamera.transform.position.x);
        if (distance > myColl.size.x * numberOfBackgrounds / 2)
        {
            if (transform.position.x < mainCamera.transform.position.x)
            {
                transform.position = new Vector2(transform.position.x + myColl.size.x * numberOfBackgrounds, transform.position.y);
            }
            else if(transform.position.x > mainCamera.transform.position.x)
            {
                transform.position = new Vector2(transform.position.x - myColl.size.x * numberOfBackgrounds, transform.position.y);
            }
        }
    }
}
