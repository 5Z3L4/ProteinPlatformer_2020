using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float shootSpeed;
    public float lifeTime;
    public Rigidbody2D rb;
    public PlayerMovement player;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
        rb.velocity = new Vector2(shootSpeed * GameObject.Find("Girl").GetComponent<LaughingGirl>().isFacingRight * Time.fixedDeltaTime, 0);
    }
    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player.hp--;
            DestroyProjectile();
        }
        
    }
}
