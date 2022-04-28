using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManagement : MonoBehaviour
{
    //MENU
    public GameObject menuPanel;
    public GameObject storyModePanel;
    public GameObject leaderboardsPanel;
    public GameObject instructionsPanel;
    public GameObject optionsPanel;
    [Space(10)]
    //LEVELS
    public GameObject[] levels;
    public int selectedLevel = 0;
    [SerializeField] private Animator[] _animators;
    [Space(10)]
    //STORY MODE
    public Button Level1;

    private SaveManager _SM;
    private Firebase _db;
    [SerializeField]
    private TMP_InputField walletInput;
    public GameObject walletPanel;

    private void Awake()
    {
        _SM = GameObject.Find("SaveManager").GetComponent<SaveManager>();
        _db = GameObject.FindGameObjectWithTag("DB").GetComponent<Firebase>();
    }
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (string.IsNullOrEmpty(_SM.terraWallet))
        {
            walletPanel.SetActive(true);
        }
    }

    public void EnterTerraWallet()
    {
        if (!string.IsNullOrEmpty(walletInput.text))
        {
            _SM.terraWallet = walletInput.text;
            _db.PostToDB(true);
            walletPanel.SetActive(false);
        }
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

    public void NextLevel()
    {
        StartCoroutine(NextLevelFade());
    }
    public void PreviousLevel()
    {
        StartCoroutine(PreviousLevelFade());
    }
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
    private IEnumerator NextLevelFade()
    {
        _animators[selectedLevel].Play("Right");
        yield return new WaitForSeconds(_animators[selectedLevel].GetCurrentAnimatorStateInfo(0).length);
        levels[selectedLevel].SetActive(false);
        selectedLevel = (selectedLevel + 1) % levels.Length;
        levels[selectedLevel].SetActive(true);
        _animators[selectedLevel].Play("Entry");
    }
    private IEnumerator PreviousLevelFade()
    {
        _animators[selectedLevel].Play("Left");
        yield return new WaitForSeconds(_animators[selectedLevel].GetCurrentAnimatorStateInfo(0).length);
        levels[selectedLevel].SetActive(false);
        selectedLevel--;
        if (selectedLevel < 0)
        {
            selectedLevel += levels.Length;
        }
        levels[selectedLevel].SetActive(true);
        _animators[selectedLevel].Play("Entry");
    }
}
