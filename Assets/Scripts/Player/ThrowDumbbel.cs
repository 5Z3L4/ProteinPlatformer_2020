using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowDumbbel : MonoBehaviour
{
    public GameObject dumbbel;
    public float startTimeBtwAttack;
    public bool canIShoot = false;
    private PlayerMovement player;
    float timeBtwAttack =0;
    Animator anim;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
        anim = GameObject.FindGameObjectWithTag("PlayerSprite").GetComponent<Animator>();
    }
    void Update()
    {
        if (!canIShoot || !player.canMove) return;

        if (timeBtwAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(Shot());
                timeBtwAttack = startTimeBtwAttack;
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
        
    }
    IEnumerator Shot()
    {
        anim.Play("ThrowSkinny");
        yield return new WaitForSeconds(0.3f);
        Instantiate(dumbbel, new Vector2(transform.position.x, transform.position.y + 1.5f), transform.rotation);
    }
}
