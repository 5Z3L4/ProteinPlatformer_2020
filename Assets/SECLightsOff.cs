using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SECLightsOff : MonoBehaviour
{
    [SerializeField]
    private InterlocutorDialogue _startDialogue;

    // Update is called once per frame
    void Update()
    {
        if (_startDialogue.isOver)
        {
            Destroy(gameObject);
        }
    }
}
