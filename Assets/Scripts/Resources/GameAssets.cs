using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;
    public static GameAssets i
    {
        get
        {
            if (_i == null)
                _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return _i;
        }
    }
    public SoundAudioClip[] soundAudioClipArray; //tablica z klipami audio
    [System.Serializable]
    public class SoundAudioClip //klasa z obiektami potrzebnymi do odtwarzania dzwieku
    {
        public SFXManager.Sound sound;
        public AudioClip audioClip;
    }
}
