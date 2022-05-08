using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class Results : MonoBehaviour
{
    public Animator sceneTransition;
    public GameObject firstSelected;
    public TMP_Text scoreResult;
    public TMP_Text vendingMachinesCount;
    public TMP_Text chestsCount;
    public TMP_Text meatsCount;
    public TMP_Text meatsCountCollected;
    public TMP_Text vendingMachinesCountCollected;
    public TMP_Text chestsCountCollected;
    public TMP_Text finalScore;
    public float timeNeededForFinishInSeconds;
    public float oneSecondValue;
    [SerializeField]
    private int currentLevelNumber;
    private SaveManager _sm;
    private PlayerMovement player;
    private float _timer;
    private float _finalScore;

    private void Awake()
    {
        _sm = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>();
        player = FindObjectOfType<PlayerMovement>();
    }

    private void OnEnable()
    {
        _timer = Time.timeSinceLevelLoad;
        player.canMove = false;
        EventSystem.current.firstSelectedGameObject = firstSelected;
        EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        meatsCountCollected.SetText(GameManager.collectedConstitution.ToString() + "/" + GameManager.collectiblesOnMap.ToString());
        vendingMachinesCountCollected.SetText(GameManager.collectedVendingMachines.ToString() + "/" + GameManager.vendingMachinesOnMap.ToString());
        chestsCountCollected.SetText(GameManager.collectedChests.ToString() + "/" + GameManager.chestsOnMap.ToString());
        scoreResult.SetText("Score: " + GameManager.Score.ToString());
        vendingMachinesCount.SetText(" x " + CalculateMultiplierByCollectedItems("vendingMachine").ToString("0.0"));
        chestsCount.SetText(" x " + CalculateMultiplierByCollectedItems("chest").ToString("0.0"));
        meatsCount.SetText(" x " + CalculateMultiplierByCollectedItems("meat").ToString("0.0"));
        _finalScore = CalculateFinalScore();
        GameManager.Score = (int)_finalScore;
        finalScore.SetText("Final score: <color=#FF0000> " + ((int)_finalScore).ToString("0") + "</color>");
        _sm.UpdateDataForCurrentLevel(currentLevelNumber, (int)_finalScore, _timer, GameManager.collectedChests, GameManager.collectedVendingMachines, GameManager.collectedConstitution ,false, _sm.playerName);
    }
    public void NextLevel(string levelName)
    {
        StartCoroutine(SceneTransition());
        SceneManager.LoadScene(levelName);
        GameManager.Reset();
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu 1");
    }
    private IEnumerator SceneTransition()
    {
        sceneTransition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
    }

    private float CalculateScoreByTime()
    {
        float additionalPoints = 0;
        if (timeNeededForFinishInSeconds - _timer > 0)
        {
            additionalPoints = (timeNeededForFinishInSeconds - _timer) * oneSecondValue;
        }
        return additionalPoints;
    }
    private float CalculateMultiplierByCollectedItems(string collectibleName)
    {
        float multiplier = 1;
        if (collectibleName == "vendingMachine" && GameManager.vendingMachinesOnMap > 0)
        {
            if (GameManager.collectedVendingMachines == GameManager.vendingMachinesOnMap)
            {
                multiplier += 0.4f;
            }
            else if (GameManager.collectedVendingMachines < GameManager.vendingMachinesOnMap && GameManager.collectedVendingMachines >= GameManager.vendingMachinesOnMap/2)
            {
                multiplier += 0.3f;
            }
            else if (GameManager.collectedVendingMachines < GameManager.vendingMachinesOnMap / 2 && GameManager.collectedVendingMachines > 0)
            {
                multiplier += 0.2f;
            }
            return multiplier;
        }
        else if (collectibleName == "chest" && GameManager.chestsOnMap > 0)
        {
            if (GameManager.collectedChests == GameManager.chestsOnMap)
            {
                multiplier += 0.4f;
            }
            else if (GameManager.collectedChests < GameManager.chestsOnMap && GameManager.collectedChests >= GameManager.chestsOnMap / 2)
            {
                multiplier += 0.3f;
            }
            else if (GameManager.collectedChests < GameManager.chestsOnMap / 2 && GameManager.collectedChests > 0)
            {
                multiplier += 0.2f;
            }
            return multiplier;
        }
        else if (collectibleName == "meat" && GameManager.collectiblesOnMap > 0)
        {
            if (GameManager.collectedConstitution == GameManager.collectiblesOnMap)
            {
                multiplier += 0.4f;
            }
            else if (GameManager.collectedConstitution < GameManager.collectiblesOnMap && GameManager.collectedConstitution >= GameManager.collectiblesOnMap / 2)
            {
                multiplier += 0.3f;
            }
            else if (GameManager.collectedConstitution < GameManager.collectiblesOnMap / 2 && GameManager.collectedConstitution > 0)
            {
                multiplier += 0.2f;
            }
            return multiplier;
        }
        else
            return multiplier;
    }
    private float CalculateFinalScore()
    {
        _finalScore = (GameManager.Score + CalculateScoreByTime()) * CalculateMultiplierByCollectedItems("vendingMachine") * CalculateMultiplierByCollectedItems("chest") * CalculateMultiplierByCollectedItems("meat");
        return _finalScore;
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
        
}
