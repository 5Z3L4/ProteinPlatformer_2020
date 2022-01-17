using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManagement : MonoBehaviour
{
    public PlayerBubbleTrigger tutorial;
    public GameObject dialogueBox;
    public GameObject responseBox;
    public Quest questForTut;
    public void EnableTutorial()
    {
        tutorial.isTutAvailable = true;
    }
    public void DisableTutorial()
    {
        tutorial.HideTutorialText();
    }
    private void Update()
    {
        if (!dialogueBox.gameObject.activeInHierarchy && !responseBox.gameObject.activeInHierarchy && questForTut.isQuestCompleted)
        {
            if (!GetComponent<Kark>().leftOrRight)
            {
                GetComponent<Kark>().Flip();
                GetComponent<Kark>().leftOrRight = !GetComponent<Kark>().leftOrRight;
            }
            EnableTutorial();
        }
    }
    private void OnDestroy()
    {
        tutorial.HideTutorialText();
    }
}
