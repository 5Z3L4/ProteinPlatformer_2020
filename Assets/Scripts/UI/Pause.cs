using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
}
