using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoRight : MonoBehaviour
{
    public GameObject Explosion;

    private Rigidbody2D _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        _rb.MovePosition(transform.position + Vector3.right * 2.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BossRage"))
        {
            SFXManager.PlaySound(SFXManager.Sound.FireworkExplosion, transform.position);
            Instantiate(Explosion, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
