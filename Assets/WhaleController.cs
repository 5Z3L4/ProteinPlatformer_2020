using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhaleController : MonoBehaviour
{
    public GameObject Bag;
    public int BossHp = 20;
    public Slider HpSlider;
    [SerializeField]
    private GameObject _ticket;
    private WhaleSpawner _whaleSpawner;
    private Animator _anim;
    private bool _isDead;
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _whaleSpawner = GetComponent<WhaleSpawner>();
    }
    private void Start()
    {
        HpSlider.maxValue = BossHp;
        HpSlider.value = BossHp;
    }

    public void Spawnbag()
    {
        Instantiate(Bag);
    }

    public void StartWave()
    {
        _anim.SetBool("Echo", true);
        _whaleSpawner.StopSpawn = false;
        _whaleSpawner.SpawnedAmount = 0;
    }

    public void GetDamage()
    {
        if (_isDead) return;
        _whaleSpawner.StopSpawn = true;
        BossHp--;
        HpSlider.value = BossHp;

        if (BossHp <= 0)
        {
            _isDead = true;
            _anim.Play("WhaleDeath");
            _ticket.SetActive(true);
            return;
        }
        _anim.SetBool("GetHit", true);
        //stopspawn true

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("x"))
        {
            GetDamage();
        }
    }
}
