using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatCoinerController : MonoBehaviour
{
    private bool _facingRight;
    private PlayerMovement _player;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerMovement>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > _player.transform.position.x)
        {
            if (_facingRight) return;
            Flip();
            _facingRight = true;
        }
        if (transform.position.x < _player.transform.position.x)
        {
            if (!_facingRight) return;
            Flip();
            _facingRight = false;
        }
    }

    public void Flip()
    {
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
