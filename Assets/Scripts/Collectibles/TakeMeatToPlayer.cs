using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeMeatToPlayer : MonoBehaviour
{
    public GameObject childSprite;
    public GameObject collectible;
    private bool shouldGoToPlayer = false;
    public float speed = 15f;
    private PlayerMovement player;
    private Rigidbody2D myRb;
    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
        myRb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (shouldGoToPlayer)
        {
            float step = speed * Time.deltaTime;

            // move sprite towards the target location
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(takeItToPlayerAfterWhile());
    }

    IEnumerator takeItToPlayerAfterWhile()
    {
        yield return new WaitForSeconds(0.6f);
        childSprite.SetActive(false);
        collectible.SetActive(true);
        myRb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.2f);
        myRb.simulated = false;
        shouldGoToPlayer = true;
        
    }
}
