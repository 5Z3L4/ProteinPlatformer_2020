using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SFXManager
{
    public enum Sound//enum z nazwami klipów
    {
        PickUpItem,
        Jump,
        DestroyChest,
        Slide,
        Death
    }
    private static Dictionary<Sound, float> soundTimerDictionary;

    public static void Initialize()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.PickUpItem] = 0f;
    }

    private static bool CanPlaySound(Sound sound) //sprawdzanie czy mozna zagrac kolejny dzwiek (zeby wyeliminowac ciagle odtwarzanie dziekow np. podczas chodzenia)
    {
        switch(sound)
        {
            default:
                return true;
            case Sound.PickUpItem:
                return true;
            case Sound.Jump:
                return true;
        }
    }

    public static void PlaySound(Sound sound, Vector2 position) //odtwarzanie dzwieku wzgledem pozycji obiektu
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound"); // tworzenie obiektu
            soundGameObject.transform.position = position;
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>(); //dodanie komponentu audiosource do obiektu
            audioSource.volume = GameObject.Find("SoundManager").GetComponent<SoundManager>().sfxVolumeSlider.value;
            audioSource.clip = GetAudioClip(sound);
            audioSource.Play(); //odtworzenie klipu jeden raz
            Object.Destroy(soundGameObject, audioSource.clip.length);
        }
    }

    //public static void PlaySound(Sound sound)
    //{
    //    if(CanPlaySound(sound))
    //    {
    //        GameObject soundGameObject = new GameObject("Sound"); // tworzenie obiektu
    //        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>(); //dodanie komponentu audiosource do obiektu
    //        audioSource.PlayOneShot(GetAudioClip(sound)); //odtworzenie klipu jeden raz
    //    }
        
    //}
    public static AudioClip GetAudioClip(Sound sound) //wyszukiwanie odpowiedniego dzwieku z tablicy
    {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipArray) //przeszukanie calej tablicy
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip; //jesli znajdzie odpowiedni klip
            }
        }
        Debug.LogError("Sound " + sound + " not found!"); //jesli nie znajdzie odpowiedniego klipu
        return null;
    }
}
