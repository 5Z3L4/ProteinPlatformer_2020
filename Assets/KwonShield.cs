using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KwonShield : MonoBehaviour
{
    [SerializeField]
    private GameObject shield;
    [SerializeField]
    private Animator _bossAnim;
    [SerializeField]
    private PlayerResponses _shieldDialogue;
    [SerializeField]
    private GameObject _kwonShield;
    [SerializeField]
    private GameObject _kwonShieldSprite;
    private bool _isShieldSpawned = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shield"))
        {
            Destroy(collision.gameObject);
            shield.SetActive(true);
            _bossAnim.SetBool("HaveShield", true);
        }
    }
    private void Update()
    {
        if (_isShieldSpawned) return;
        if (_shieldDialogue.isOver)
        {
            _isShieldSpawned = true;
            _kwonShieldSprite.SetActive(false);
            _kwonShield.SetActive(true);
        }
    }
}
