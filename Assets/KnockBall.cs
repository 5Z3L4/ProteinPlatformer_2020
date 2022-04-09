using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBall : MonoBehaviour
{
    [SerializeField]
    private GameObject Bomb;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Destroy(collision.gameObject);
            Instantiate(Bomb, position: transform.position, Quaternion.identity);
        }
    }
}
