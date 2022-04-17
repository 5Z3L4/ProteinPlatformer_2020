using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SECSkinnyController : MonoBehaviour
{
    [SerializeField]
    private GameObject dumbbel;
    [SerializeField]
    private InterlocutorDialogue _throwDialogue;
    private Animator _anim;
    private bool _isDumbbelThrowed = false;
    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (_isDumbbelThrowed) return;
        if (_throwDialogue.isOver)
        {
            _isDumbbelThrowed = true;
            _anim.Play("SecThrowSkinny");
        }
    }
    public void ThrowDumbbel()
    {
        dumbbel.SetActive(true);
    }
}
