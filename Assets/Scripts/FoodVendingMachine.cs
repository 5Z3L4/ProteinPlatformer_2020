using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodVendingMachine : MonoBehaviour
{
    public GameObject MeatHolder;
    public ShakeCollectible ShakeVending;
    private PlayerMovement _player;
    private bool _isUsed = false;
    private void Awake()
    {
        _player = FindObjectOfType<PlayerMovement>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_isUsed) return;
        if (collision.CompareTag("Player") && (_player.isSliding || _player.isCharging))
        {
            MeatHolder.SetActive(true);
            _isUsed = true;
            ShakeVending.ShakeOnce();
        }
    }
}
