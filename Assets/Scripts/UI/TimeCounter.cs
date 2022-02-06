using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    public TextMeshProUGUI counterText;
    public float seconds;
    public float minutes;
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
}
