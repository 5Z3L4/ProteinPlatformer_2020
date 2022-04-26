using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnShieldTutorialFinish : MonoBehaviour
{
    public Animator whaleAnim;
    public GameObject tutorialPage;

    private bool _shouldToggleBool = true;

    void Update()
    {
        if (_shouldToggleBool)
        {
            if (!tutorialPage.activeInHierarchy)
            {
                whaleAnim.SetBool("StartDialogue", true);
                _shouldToggleBool = false;
            }
        }
    }
}
