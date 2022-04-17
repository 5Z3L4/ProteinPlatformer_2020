using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SECArnieController : MonoBehaviour
{
    [SerializeField]
    private InterlocutorDialogue _shieldDialogue;
    [SerializeField]
    private GameObject _kwonShield;
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private Animator _shieldAnim;
    private bool _isShieldSpawned = false;

    // Update is called once per frame
    void Update()
    {
        if (_isShieldSpawned) return;
        if (_shieldDialogue.isOver)
        {
            _isShieldSpawned = true;
            Instantiate(_kwonShield);
        }
    }
}
