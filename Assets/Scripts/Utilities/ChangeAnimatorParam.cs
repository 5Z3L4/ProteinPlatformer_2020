using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAnimatorParam : MonoBehaviour
{
    public string BoolToChange;
    public bool Change;
    public Animator anim;
    void Update()
    {
        if (Change)
        {
            anim.SetBool(BoolToChange, true);
            Change = false;
        }
    }
}
