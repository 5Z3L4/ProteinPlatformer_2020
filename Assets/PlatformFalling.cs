using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFalling : MonoBehaviour
{
    Rigidbody2D myRb;
    // Start is called before the first frame update
    void Awake()
    {
        myRb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke("DropPlatform", 0.5f);

        }
    }
    void DropPlatform()
    {
        myRb.isKinematic = false;
    }
}
