using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TimeCounter : MonoBehaviour
{
    public TextMeshProUGUI counterText;
    public float seconds;
    public float minutes;
    [System.Serializable]
    public struct Thresholds
    {
        public float Threshold;
        public float PointsToAdd;
    }
    public Thresholds[] thresholds;
    void Awake()
    {
        counterText = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        minutes = (int)(Time.timeSinceLevelLoad / 60f);
        seconds = (int)(Time.timeSinceLevelLoad % 60f);
        counterText.SetText(minutes.ToString("00") + ":" + seconds.ToString("00"));
    }
    public void AddPointsForTime()
    {
        if (minutes < thresholds[0].Threshold)
        {
            GameManager.Score += (int)thresholds[0].PointsToAdd;
        }
        else if (minutes >= thresholds[0].Threshold && minutes < thresholds[1].Threshold)
        {
            GameManager.Score += (int)thresholds[1].PointsToAdd;
        }
        else if (minutes >= thresholds[1].Threshold && minutes < thresholds[2].Threshold)
        {
            GameManager.Score += (int)thresholds[2].Threshold;
        }
        else
        {
            GameManager.Score += (int)thresholds[3].PointsToAdd;
        }
    }
}
