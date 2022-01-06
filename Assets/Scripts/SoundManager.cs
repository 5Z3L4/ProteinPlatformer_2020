using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Toggle muteToggle;
    public Slider volumeSlider;
    private bool muted = false;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        muteToggle = FindObjectOfType<Toggle>();
        volumeSlider = FindObjectOfType<Slider>();
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1f);
        }
        else if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
        }
        else
        {
            LoadAudioSettings();
        }
        if (GameObject.Find("OptionsPanel") != null)
        {
            GameObject.Find("OptionsPanel").SetActive(false);
        }
        
    }

    public void MuteToggle()
    {
        if (!muted)
        {
            muted = true;
            AudioListener.pause = true;
        }
        else
        {
            muted = false;
            AudioListener.pause = false;
        }
    }

    public void SetVolume()
    {
        AudioListener.volume = volumeSlider.value;
        SaveAudioSettings();
    }

    private void LoadAudioSettings()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        muted = PlayerPrefs.GetInt("muted") == 1;
    }

    private void SaveAudioSettings()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }
}
