using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleBag : MonoBehaviour
{
    private PlayerMovement _player;
    private void Awake()
    {
        _player = GameObject.FindObjectOfType<PlayerMovement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _player.TakeCertainAmountOfHp();
        }
    }
    public void DestroyBag()
    {
        ScreenShake.Instance.Shakecamera(5f, .1f);
        Destroy(gameObject);
    }
}
