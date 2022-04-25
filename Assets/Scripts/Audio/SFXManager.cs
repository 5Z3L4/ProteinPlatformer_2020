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
        Death,
        Fail,
        BossBoom,
        Punch,
        Step,
        GetHit,
        Hit,
        Boost,
        ShibaDeath,
        BalloonBlowUp,
        MetalHit,
        Bark,
        ChaliceBreak,
        ShibaMutantGrowl,
        FireworkExplosion,
        WhipeStrike,
        FireworkWhistle,
        ShibaPullInBall,
        Thunder,
        BagHit,
        WhaleWave
    }
    private static Dictionary<Sound, float> soundTimerDictionary;

    public static void Initialize()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.PickUpItem] = 0;
        soundTimerDictionary[Sound.Step] = 0;
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
            case Sound.Step:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float playerMoveTimerMax = 0.4f;
                    if (lastTimePlayed + playerMoveTimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
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
