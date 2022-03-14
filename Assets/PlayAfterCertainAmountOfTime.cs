using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAfterCertainAmountOfTime : MonoBehaviour
{
    public float TimeDuringPlaythroughs = 0.5f;
    private float _currentTime =  0;
    AudioSource _audio;
    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_audio.isPlaying) return;

        if (_currentTime <= 0)
        {
            PlayAudio();
            _currentTime = TimeDuringPlaythroughs;
        }
        else
        {
            _currentTime -= Time.deltaTime;
        }
    }

    private void PlayAudio()
    {
        _audio.Play();
    }
}
