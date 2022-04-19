using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastSecController : MonoBehaviour
{
    public bool Shot = false;
    [SerializeField]
    private GameObject _fireWorks;
    [SerializeField]
    private InterlocutorDialogue _dialogue;
    private Animator _beastAnim;
    private bool _isOver = false;

    private void Awake()
    {
        _beastAnim = GetComponent<Animator>();
    }
    void Update()
    {
        if (_isOver) return;
        if (!Shot) return;
        if (_dialogue.isOver)
        {
            _beastAnim.Play("BeastFireworks");
            _fireWorks.SetActive(true);
            _isOver = true;
        }
    }
}
