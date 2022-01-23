using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    [SerializeField] private AudioSource musicSource;
    private void Awake()
    {
        SFXManager.Initialize();
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
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }
    public void ChangeMusicVolume()
    {
        musicSource.volume = musicVolumeSlider.value;
        SaveMusic();
    }
    public void ChangeSFXVolume()
    {
        SaveSFX();
    }
    private void Load()
    {
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
    }
    private void SaveMusic()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
    }
    private void SaveSFX()
    {
        PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);
    }
}
