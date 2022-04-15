using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SECController : MonoBehaviour
{
    [SerializeField]
    private GameObject _left;
    [SerializeField]
    private GameObject _right;
    private Animator _anim;
    private int _shotCOunter = 0;
    private int _hitCOunter = 0;
    private void Awake()
    {
        _anim = GetComponent<Animator>();
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("DogEnemy"))
        {
            _hitCOunter++;
            if (_hitCOunter >= 3)
            {
                //zrzuæ œmiecia
                _shotCOunter = 0;
                _anim.SetBool("Fall", true);
            }
        }
    }
}
