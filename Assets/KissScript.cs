using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KissScript : MonoBehaviour
{
    private Animator _anim;
    [SerializeField]
    private GameObject _player;
    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _anim.Play("KissSkinny");
            _player.SetActive(false);
        }
    }
}
