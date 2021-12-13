using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManagement : MonoBehaviour
{

    public AudioMixer audioMixer; // tworzymy obiekt typu AudioMixer z biblioteki
    public Toggle muteToggle;
    public Slider slider;

    public void Mute()
    {
        if(muteToggle.isOn)
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
            audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20); // dla parametru volume z naszego obiektu odpowiedzialnego za audio ustawiamy wczeœniej otrzyman¹ wartoœæ z floata ze slidera (obliczan¹ z logarytmu)
    }
    void Awake()
    {
        if (ticker.counter == 0)
        {
            DontDestroyOnLoad(this.gameObject); //zapobieganie usuwaniu obiektu SoundManagement podczas ³adowania innych scen bez duplikowania przy powrocie
            ticker.counter++;
            return;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
