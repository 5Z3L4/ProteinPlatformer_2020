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
    //PROFILE
    public GameObject[] characters;
    public int selectedCharacter = 0;
    public void NextCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % characters.Length;
        characters[selectedCharacter].SetActive(true);
    }
    public void PreviousCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if(selectedCharacter < 0)
        {
            selectedCharacter += characters.Length;
        }
        characters[selectedCharacter].SetActive(true);
    }
    public void ChooseCharacter()
    {
        PlayerPrefs.SetInt("Selected Character", selectedCharacter);
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
