using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool gamePaused = false;
    [SerializeField] private GameObject pauseScreen;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                pauseScreen.gameObject.SetActive(false);
                gamePaused = false;
                Time.timeScale = 1;
            }
            else
            {
                pauseScreen.gameObject.SetActive(true);
                gamePaused = true;
                Time.timeScale = 0;
            }
            
        }
    }
}
