using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuscleDogeController : MonoBehaviour
{
    public int BossHp = 10;
    public Slider HpSlider;
    private Animator _anim;
    public GameObject Ticket;
    private void Awake()
    {
        HpSlider.maxValue = BossHp;
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {
        HpSlider.value = BossHp;
    }

    public void GetDamage()
    {
        BossHp--;
        HpSlider.value = BossHp;
        if (BossHp <= (5))
        {
            _anim.SetBool("2ndState", true);
        }
        if (BossHp <= 0)
        {
            Ticket.SetActive(true);
            _anim.SetBool("IsDead", true);
            return;
        }
        _anim.SetBool("GetHit", true);
    }

}
