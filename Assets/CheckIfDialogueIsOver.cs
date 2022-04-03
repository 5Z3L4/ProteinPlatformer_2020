using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfDialogueIsOver : MonoBehaviour
{
    public InterlocutorDialogue Dialogue;
    public Animator anim;
    private bool _isBoolChanged = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isBoolChanged) return;
        if (Dialogue.isOver)
        {
            anim.SetBool("Rocket", true);
            if (anim.GetBool("Rocket"))
            {
                _isBoolChanged = true;
            }
        }
    }
}
