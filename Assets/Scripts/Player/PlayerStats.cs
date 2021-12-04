using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    public float strengthCap;
    public float agilityCap;
    public float constitutionCap;

    public float currentStrength;
    public float currentAgility;
    public float currentConstitution;

    [SerializeField]
    static Transform spawnposition;

    public float moveSpeed = 300;
    public float chargeSpeed = 700;
    public float slideSpeed = 450;
    public float jumpForce = 5;
    public Transform playerPosition;
    public Vector3 spawnPosition;

    public int maxHealth = 5;
    public bool isDeadBySpikes = false;
    public int healthPoints = 5;
    public  bool isDead = false;
    public  int souls;
    public  float attackRange = 0.5f;
    public  int damage = 1;

    private static void Awake()
    {
        //Object.DontDestroyOnLoad(gameObject);
        //anim = GetComponent<Animator>();
    }
    //leci do playerManager
    private static void Update()
    {
        //if (healthPoints <= 0 && !isDead)
        //{
        //    isDead = true;
        //    //SoundManager.PlaySound(SoundManager.Sound.PlayerDie);
        //    //anim.Play("PlayerDeath");
        //}

    }
    //to samo
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spikes"))
        {
            isDeadBySpikes = true;
            healthPoints = 0;
        }
        //if (collision.CompareTag("Resp"))
        //{
        //    spawnposition = collision.transform;
        //    GameObject.FindGameObjectWithTag("Level1").SetActive(false);
        //    GameObject.FindGameObjectWithTag("Level2").SetActive(true);
        //}
    }
    //te¿
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Spikes"))
        {
            isDeadBySpikes = false;
        }
    }
    
}
