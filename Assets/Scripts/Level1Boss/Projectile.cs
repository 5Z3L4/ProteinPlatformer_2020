using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float shootSpeed;
    public PlayerMovement player;

    private Vector3 targetPos;
    private Vector3 startingPos;
    private bool startMoving = false;
    private ParticleSystem particleSystem;
    private bool shouldPlayParticle = true;


    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }
    private void Start()
    {
        startingPos = transform.position;
    }
    private void OnEnable()
    {
        targetPos = player.transform.position;
        startMoving = true;
    }
    private void Update()
    {
        if (startMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, shootSpeed * Time.deltaTime);
            if (transform.position == targetPos && shouldPlayParticle)
            {
                shouldPlayParticle = false;
                StartCoroutine(Destroy());
            }
        }
    }
    private void OnDisable()
    {
        transform.position = startingPos;
        startMoving = false;    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(Destroy());
            player.TakeCertainAmountOfHp();
        }
    }

    IEnumerator Destroy()
    {
        particleSystem.Play();
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
