using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArnieShield : MonoBehaviour
{
    private Animator _anim;
    [SerializeField]
    private BoxCollider2D _shieldColl;
    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            _anim.Play("ArnieShield");
            _shieldColl.enabled = true;
        }
        else
        {
            _shieldColl.enabled = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (true)
        {

        }
       //niszczy pilke
       //respi nowa pilke ktora leci w walenia
    }
}
