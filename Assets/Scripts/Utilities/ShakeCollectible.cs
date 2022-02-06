using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCollectible : MonoBehaviour
{
    public float speed = 2f;
    public float maxRotation = 45f;
    public bool ShakeAutomaticly = true;
    private bool _startShaking = false;
    [SerializeField]private float _shakeTimer;

    void Update()
    {
        if (ShakeAutomaticly)
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, maxRotation * Mathf.Sin(Time.time * speed));
        }
        else
        {
            if (_startShaking)
            {
                if (_shakeTimer >= 0)
                {
                    transform.localRotation = Quaternion.Euler(0f, 0f, maxRotation * Mathf.Sin(Time.time * speed));
                    _shakeTimer -= Time.deltaTime;
                }
            }
        }
    }

    public void ShakeOnce()
    {
        _startShaking = true;
    }
}
