using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KissScript : MonoBehaviour
{
    public KissType KissAnim;
    private Animator _anim;
    [SerializeField]
    private GameObject _player;
    public enum KissType
    {
        KissSkinny,
        KissMuscular,
        KissBeast,
        KissArnie,
        KissKwon
    }
    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _anim.Play(KissAnim.ToString());
            _player.SetActive(false);
        }
    }
}
