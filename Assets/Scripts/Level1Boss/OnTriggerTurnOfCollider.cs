using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerTurnOfCollider : MonoBehaviour
{
    public CircleCollider2D collToTurnOff;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collToTurnOff.isTrigger = true;
        }
    }
}
