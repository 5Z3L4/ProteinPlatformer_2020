using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManagement : MonoBehaviour
{
    //MENU
    public GameObject menuPanel;
    public GameObject storyModePanel;
    public GameObject leaderboardsPanel;
    public GameObject instructionsPanel;
    public GameObject optionsPanel;
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnClick(GameObject panel)
    {
        if(menuPanel.activeInHierarchy)
        {
            menuPanel.SetActive(false);
            panel.SetActive(true);
        }
    }
    private void DisablePanels()
    {
        menuPanel.SetActive(true);
        storyModePanel.SetActive(false);
        leaderboardsPanel.SetActive(false);
        instructionsPanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    [Space(10)]
    //LEVELS
    public GameObject[] levels;
    public int selectedLevel = 0;
    public void NextLevel()
    {
        levels[selectedLevel].SetActive(false);
        selectedLevel = (selectedLevel + 1) % levels.Length;
        levels[selectedLevel].SetActive(true);
    }
    public void PreviousLevel()
    {
        levels[selectedLevel].SetActive(false);
        selectedLevel--;
        if(selectedLevel < 0)
        {
            selectedLevel += levels.Length;
        }
        levels[selectedLevel].SetActive(true);
    }
    [Space(10)]
    //STORY MODE
    public Button Level1;
    public void LaodScene(string sceneName)
    {
        GameManager.Reset();
        SceneManager.LoadScene(sceneName);
    }
    public void ChangeMode(GameObject panel)
    {
        DisablePanels();
        menuPanel.SetActive(false);
        leaderboardsPanel.SetActive(true);
    }
    //------------------------------------------------------------------------
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DisablePanels();
        }
    }
}
