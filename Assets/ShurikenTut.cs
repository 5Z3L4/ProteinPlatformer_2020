using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenTut : MonoBehaviour
{
    public ActivateTutorial tut;
    public InterlocutorDialogue lastDial;
    private bool _shouldActivateTut = true;
    void Update()
    {
        if (_shouldActivateTut)
        {
            if (tut != null && lastDial != null)
            {
                if (lastDial.isOver)
                {
                    tut.gameObject.SetActive(true);
                    _shouldActivateTut = false;
                }
            }
            
        }
    }
}
