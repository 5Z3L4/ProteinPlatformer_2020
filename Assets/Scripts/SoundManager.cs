using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    public AudioMixer audioMixer;
    public Toggle muteToggle;
    public Slider slider;

    public void Mute()
    {
        if (muteToggle.isOn)
        {
            AudioListener.volume = 0;
            Debug.Log(AudioListener.volume);
        }
        else
        {
            AudioListener.volume = 1;
        }
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }
    void Awake()
    {
        if (Ticker.counter == 0)
        {
            DontDestroyOnLoad(this.gameObject);
            Ticker.counter++;
            return;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
public static class Ticker
{
    public static int counter = 0;
}
