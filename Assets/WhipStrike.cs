using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipStrike : MonoBehaviour
{
    public GameObject WhipeCollider;
    private Animator _anim;
    private float _startTimer = 0.5f;
    private float _time;
    private bool _isAnimPlaying = false;
    private void Awake()
    {
        _anim = GameObject.FindGameObjectWithTag("PlayerSprite").GetComponent<Animator>();
    }
    void Update()
    {
        
        if (_isAnimPlaying)
        {
            if (_time <= 0)
            {
                WhipeCollider.SetActive(false);
                _isAnimPlaying = false;
            }
            else
            {
                _time -= Time.deltaTime;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                _anim.Play("PumpedBeastWhipStrike");
                StartCoroutine(showCollider());
                _isAnimPlaying = true;
                _time = _startTimer;
                SFXManager.PlaySound(SFXManager.Sound.WhipeStrike, transform.position);
            }
        }
        
    }

    IEnumerator showCollider()
    {
        yield return new WaitForSeconds(0.3f);
        WhipeCollider.SetActive(true);
    }
}
