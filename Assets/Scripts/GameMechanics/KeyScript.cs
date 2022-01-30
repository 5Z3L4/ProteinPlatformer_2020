using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    [Tooltip("Collider of doors which should be opend via this key")]
    public DoorScript doorsToOpen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            doorsToOpen.OpenDoors();
        }
    }
}
