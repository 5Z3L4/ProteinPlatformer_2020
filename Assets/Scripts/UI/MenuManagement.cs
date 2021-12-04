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
    public GameObject endlessModePanel;
    public GameObject profilePanel;
    public GameObject optionsPanel;

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
        endlessModePanel.SetActive(false);
        profilePanel.SetActive(false);
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
    //OPTIONS
    public Button audioButton;
    public Button controlsButton;
    public GameObject audioSettingsPanel;
    public GameObject controlsSettingsPanel;
    public void EnableOptionsPanel(GameObject panelToEnable)
    {

        if (panelToEnable == audioSettingsPanel && !audioSettingsPanel.activeInHierarchy)
        {
            controlsSettingsPanel.SetActive(false);
            audioSettingsPanel.SetActive(true);
        }
        else if (panelToEnable == controlsSettingsPanel && !controlsSettingsPanel.activeInHierarchy)
        {
            audioSettingsPanel.SetActive(false);
            controlsSettingsPanel.SetActive(true);
        }
        
    }
    [Space(10)]
    //STORY MODE
    public Button Level1;
    public void LaodScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ChangeMode(GameObject panel)
    {
        DisablePanels();
        menuPanel.SetActive(false);
        endlessModePanel.SetActive(true);
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
