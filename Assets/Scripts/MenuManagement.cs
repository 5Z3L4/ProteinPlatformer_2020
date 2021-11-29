using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Image[] characters;
    public void NextCharacter()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DisablePanels();
        }
    }
}
