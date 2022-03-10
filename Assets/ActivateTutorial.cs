using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTutorial : MonoBehaviour
{
    public int tutorialID;

    private bool shouldShowTutorial = true;
    private TutorialPages tutorialPages;
    private void Awake()
    {
        tutorialPages = FindObjectOfType<TutorialPages>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (shouldShowTutorial)
            {
                tutorialPages.ActivateTutorialPage(tutorialID);
                shouldShowTutorial = false;
            }
        }
    }
}
