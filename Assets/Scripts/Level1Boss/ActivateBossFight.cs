using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBossFight : MonoBehaviour
{
    public GameObject boss;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            boss.SetActive(true);
            Destroy(gameObject);
        }
    }
}
