using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SECFireworkController : MonoBehaviour
{
    public SECController Boss;
    [SerializeField]
    private Animator _anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("x"))
        {
            Boss.GetDamage();
            _anim.SetBool("FallRight", true);
            Destroy(gameObject);
        }
    }
}
