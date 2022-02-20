using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveItem : MonoBehaviour
{
    public GameObject itemToGive;
    public InterlocutorDialogue lastDialogue;

    private void Update()
    {
        if (lastDialogue.isOver)
        {
            if (itemToGive != null)
            {
                itemToGive.SetActive(true);
            }
            else
            {
                this.enabled = false;
            }
        }
    }
}
