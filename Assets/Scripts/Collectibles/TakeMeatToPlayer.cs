using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeMeatToPlayer : MonoBehaviour
{
    public bool turnOffRb = true;
    public GameObject childSprite;
    public GameObject collectible;
    private bool shouldGoToPlayer = false;
    public float speed = 15f;
    public float WaitTimeBeforeStop = 0.6f;
    public float WaitTimeBeforeFly = 0.2f;
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
        yield return new WaitForSeconds(WaitTimeBeforeStop);
        if (childSprite != null && collectible != null)
        {
            childSprite.SetActive(false);
            collectible.SetActive(true);
        }
        if (turnOffRb)
        {
            myRb.velocity = Vector2.zero;
        }
        yield return new WaitForSeconds(WaitTimeBeforeFly);
        if (turnOffRb)
        {
            myRb.simulated = false;
        }
        shouldGoToPlayer = true;
        
    }
}
