using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SECController : MonoBehaviour
{
    public bool MuscularAttack = true;
    [SerializeField]
    private GameObject _left;
    [SerializeField]
    private GameObject _right;
    [SerializeField]
    private GameObject _fireWorks;
    [SerializeField]
    private Slider _hpSlider;
    [SerializeField]
    private Animator _muscularAnim;
    [SerializeField]
    private Animator _beastAnim;
    private Animator _anim;
    [SerializeField]
    private int _hp = 10;
    private int _shotCOunter = 0;
    private int _hitCOunter = 0;
    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    private void Start()
    {
        _hpSlider.maxValue = _hp;
        _hpSlider.value = _hp;
    }
    public void SpawnRightBriefCase()
    {
        Instantiate(_right);
    }
    public void SpawnRLeftBriefCase()
    {
        Instantiate(_left);
    }

    public void ChoseSide()
    {
        if (_shotCOunter > 3) return;
        if (Random.Range(0,2) > 0)
        {
            _anim.SetBool("Left", true);
        }
        else
        {
            _anim.SetBool("Right", true);
        }
        _shotCOunter++;
    }

    public void GetDamage()
    {
        _hp--;
        _hpSlider.value = _hp;
    }

    public void Fall(string side)
    {
        _anim.SetBool(side, true);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("DogEnemy"))
        {
            _hitCOunter++;
            if (_hitCOunter >= 3)
            {
                //zrzuæ œmiecia
                _shotCOunter = 0;
                _hitCOunter = 0;
                _anim.SetBool("Fall", true);
                if (MuscularAttack)
                {
                    _muscularAnim.Play("KickBall");
                    MuscularAttack = false;
                }
                else
                {
                    _beastAnim.Play("BeastFireworks");
                    _fireWorks.SetActive(true);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shield") || collision.CompareTag("Shield2") || collision.CompareTag("Firework"))
        {
            GetDamage();
        }
        if (collision.CompareTag("Hantel"))
        {
            GetDamage();
            Destroy(collision.gameObject);
        }
    }
}
