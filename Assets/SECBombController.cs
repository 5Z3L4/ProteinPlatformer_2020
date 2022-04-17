using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SECBombController : MonoBehaviour
{
    [SerializeField]
    private SECController _sec;
    private ParticleSystem _particleSystem;


    private void Awake()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }
    public void Explode()
    {
        SFXManager.PlaySound(SFXManager.Sound.BalloonBlowUp, transform.position);
        _sec.GetDamage();
        _sec.Fall("FallLeft");
        GetComponent<SpriteRenderer>().enabled = false;
        _particleSystem.Play();
        Destroy(gameObject, 0.15f);
    }
}
