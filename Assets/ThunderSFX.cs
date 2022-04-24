using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderSFX : MonoBehaviour
{
    private float _timer;
    
    private void Update()
    {
        if (_timer < 0)
        {
            SFXManager.PlaySound(SFXManager.Sound.Thunder, transform.position);
            _timer = Random.Range(4,12);
        }
        else
        {
            _timer -= Time.deltaTime;
        }
    }
}
