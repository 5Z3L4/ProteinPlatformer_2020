using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SECHitColliders : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("DogEnemy"))
        {
            print("Dostal po: " + gameObject.name);
        }
    }
}
