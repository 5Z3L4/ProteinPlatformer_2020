using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider SfxVolumeSlider;
    [SerializeField] private AudioSource musicSource;
    public Slider SFXVolumeSlider => SfxVolumeSlider;
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 1);
        }
        if (!PlayerPrefs.HasKey("SFXVolume"))
        {
            PlayerPrefs.SetFloat("SFXVolume", 1);
        }
        Load();
    }
    private void Start()
    {
        musicSource.volume = musicVolumeSlider.value;
    }
    public void ChangeVolume()
    {
        musicSource.volume = musicVolumeSlider.value;
        if (musicVolumeSlider.value == 0)
        {
            musicSource.mute = true;
            PlayerPrefs.SetInt("MusicMuted", 1);
        }
        else
        {
            musicSource.mute = false;
            PlayerPrefs.SetInt("MusicMuted", 0);
        }
        Save();
    }
    private void Load()
    {
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        SfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", SfxVolumeSlider.value);
    }
}
