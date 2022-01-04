using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    public TextMeshProUGUI counterText;
    public float seconds { get; private set; }
    public float minutes { get; private set; }
    void Start()
    {
        counterText = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        minutes = (int)(Time.time / 60f);
        seconds = (int)(Time.time % 60f);
        counterText.SetText(minutes.ToString("00") + ":" + seconds.ToString("00"));
    }
}
