using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SECBombController : MonoBehaviour
{
    public GameObject bomb;
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
        bomb.GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        _particleSystem.Play();
        Destroy(gameObject, 0.15f);
    }
}
