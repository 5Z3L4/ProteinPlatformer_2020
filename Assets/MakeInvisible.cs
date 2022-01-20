using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeInvisible : MonoBehaviour
{
    HiddenPlace hiddenPlace;
    private void Awake()
    {
        hiddenPlace = FindObjectOfType<HiddenPlace>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hiddenPlace.isPlayerInside) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            hiddenPlace.CheckIfPlayerIsInside();
        }     
    }
}
