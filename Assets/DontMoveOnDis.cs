using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontMoveOnDis : MonoBehaviour
{
    private PlayerMovement _player;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerMovement>();
    }

    private void OnDisable()
    {
        _player.canMove = false;
    }
}
