using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbbelFly : MonoBehaviour
{
    public float speed = 10;
    bool right;
    [SerializeField] private GameObject particle;
    // Start is called before the first frame update
    private void Awake()
    {
        right = FindObjectOfType<PlayerMovement>().facingRight;
    }

    // Update is called once per frame
    void Update()
    {
        if (right)
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(-transform.right * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Instantiate(particle, transform.position, Quaternion.identity);
    }
}
