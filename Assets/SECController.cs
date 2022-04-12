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
        if (Random.Range(0,2) > 0)
        {
            _anim.SetBool("Left", true);
        }
        else
        {
            _anim.SetBool("Right", true);
        }
        _shotCOunter++;
        if (_shotCOunter >=3)
        {
            //zrzuæ œmiecia
            _shotCOunter = 0;
            print("Spad³o");
        }
    }
}
