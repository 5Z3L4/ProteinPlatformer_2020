using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    [SerializeField]
    static Transform spawnposition;

    public static float moveSpeed = 300;
    public static float jumpForce = 5;
    public static Transform playerPosition;
    public static Vector3 spawnPosition;

    public static int maxHealth = 5;
    public static bool isDeadBySpikes = false;
    public static int healthPoints = 5;
    private static Animator anim;
    public static bool isDead = false;
    public static int souls;
    public static float attackRange = 0.5f;
    public static int damage = 1;

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
    private static void OnTriggerEnter2D(Collider2D collision)
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
    private static void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Spikes"))
        {
            isDeadBySpikes = false;
        }
    }
    
}
