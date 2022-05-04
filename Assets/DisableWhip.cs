using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisableWhip : MonoBehaviour
{
    private InterlocutorDialogue _lastDialogue;
    private TMP_Text _text;
    private bool _shouldDisplayText = true;
    private void Awake()
    {
        _lastDialogue = GameObject.Find("Ofc").GetComponent<InterlocutorDialogue>();
        _text = GetComponent<TMP_Text>();
    }
    void Update()
    {
        if (_lastDialogue.isOver)
        {
            if (_shouldDisplayText)
            {
                _text.SetText("Press Z to use whip");
                _shouldDisplayText = false;
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Destroy(gameObject);
            }
        }
    }
}
