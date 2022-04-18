using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SECMuscularController : MonoBehaviour
{
    [SerializeField]
    private Animator _ballAnim;
    [SerializeField]
    private InterlocutorDialogue _dialogue;
    private bool _isOver = false;
    private void Update()
    {
        if (_isOver) return;
        if (_dialogue.isOver)
        {
            _isOver = true;
            StartBallAnim();
        }
    }
    public void StartBallAnim()
    {
        _ballAnim.Play("KickSec");
    }
}
