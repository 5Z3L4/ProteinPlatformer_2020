using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipStrike : MonoBehaviour
{
    public GameObject WhipeCollider;
    private Animator _anim;
    private void Awake()
    {
        _anim = GameObject.FindGameObjectWithTag("PlayerSprite").GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _anim.Play("PumpedBeastWhipStrike");
            WhipeCollider.SetActive(true);
        }
    }
}
