using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KonamiCode : MonoBehaviour
{
    PlayerMovement player;
    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
    }
    private KeyCode[] moon = new KeyCode[]
    {
        KeyCode.M,
        KeyCode.O,
        KeyCode.O,
        KeyCode.N
    };
    int currentKeyIndex = 0;
    void Update()
    {
        OnInput(DetectKeyPressed());
    }
    void OnInput(KeyCode KeyCodeValue)
    {
        if (KeyCodeValue == KeyCode.None) return;

        if (KeyCodeValue == moon[currentKeyIndex])
        {
            currentKeyIndex++;
            if (currentKeyIndex >= moon.Length)
            {
                MoonWalk();
                currentKeyIndex = 0;
            }
        }
        else
        {
            currentKeyIndex = 0;
        }
    }

    void MoonWalk()
    {
        player.KonamiMoonWalk = true;
    }
    private KeyCode DetectKeyPressed()
    {
        foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                return key;
            }
        }
        return KeyCode.None;
    }


}
