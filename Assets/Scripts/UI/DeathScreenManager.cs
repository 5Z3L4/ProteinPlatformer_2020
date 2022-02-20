using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class DeathScreenManager : MonoBehaviour
{
    [SerializeField] private TMP_Text ScoreText;
    [SerializeField] private TMP_Text DeathCounterText;
    [SerializeField] private GameObject restartButton;
    private float score;
    private int deaths;

    private void Start()
    {
        score = GameManager.Score;
        deaths = GameManager.deaths;
    }

    private void Update()
    {
        score = GameManager.Score;
        deaths = GameManager.deaths;

        ScoreText.SetText("Score: \n" + score.ToString());
        DeathCounterText.SetText("Deaths: \n" + deaths.ToString());
        if (gameObject.activeInHierarchy)
        {
            EventSystem.current.SetSelectedGameObject(restartButton);
        }
    }
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(restartButton);
    }
    private void OnDisable()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
