using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SECFireworkController : MonoBehaviour
{
    public ParticleSystem explode;
    public SECController Boss;
    [SerializeField]
    private Animator _anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("x"))
        {
            Boss.GetDamage();
            explode.Play();
            _anim.SetBool("FallRight", true);
            Invoke("Destroy", 1f);
        }
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
