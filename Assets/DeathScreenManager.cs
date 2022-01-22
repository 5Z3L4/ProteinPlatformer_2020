using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathScreenManager : MonoBehaviour
{
    [SerializeField] private TMP_Text ScoreText;
    [SerializeField] private TMP_Text DeathCounterText;
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
    }
}
