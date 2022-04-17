using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SECMuscularController : MonoBehaviour
{
    [SerializeField]
    Animator _ballAnim;
    public void StartBallAnim()
    {
        _ballAnim.Play("KickSec");
    }
}
