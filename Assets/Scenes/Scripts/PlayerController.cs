using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void Awake()
    {
        PlayerStats.spawnPosition = transform.position;
    }

    private void Update()
    {
        if (PlayerStats.healthPoints <= 0)
        {
            Debug.Log("You are dead");
            PlayerStats.isDead = true;
        }
    }
    //Nie wiem jeszcze jak obs³u¿ymy œmieræ, wyjdzie w praniu na razie uznajmy ¿e mo¿e byæ
    public void Resurrect()
    {
        if (!PlayerStats.isDeadBySpikes && PlayerStats.souls > 0)
        {
            PlayerStats.healthPoints = PlayerStats.maxHealth;
            PlayerStats.isDead = false;
            //anim.Play("PlayerResurection");
            PlayerStats.souls -= 1;
        }
    }

    public static void ResurrectAtSpawn()
    {
        Debug.Log("Armand chuj");
        PlayerStats.healthPoints = PlayerStats.maxHealth;
        Debug.Log(PlayerStats.spawnPosition);
        FindObjectOfType<PlayerController>().transform.position = PlayerStats.spawnPosition;
        PlayerStats.isDead = false;
        //anim.Play("PlayerResurection");

    }
    #region boosty postaci po œmierci
    public void AddHealth()
    {
        if (PlayerStats.souls > 0)
        {
            PlayerStats.maxHealth++;
            PlayerStats.healthPoints = PlayerStats.maxHealth;
            PlayerStats.souls -= 1;
        }

    }

    public static void AddMoveSpeed()
    {
        if (PlayerStats.souls >= 1)
        {
            PlayerStats.moveSpeed += 10;
            PlayerStats.souls -= 1;
        }

    }

    public static void AddJumpForce()
    {
        if (PlayerStats.souls >= 1)
        {
            PlayerStats.jumpForce += 0.1f;
            PlayerStats.souls -= 1;
        }

    }

    public void AddDamage()
    {
        if (PlayerStats.souls >= 1)
        {
            PlayerStats.damage++;
            PlayerStats.souls -= 1;
        }

    }
    public void AddRange()
    {
        PlayerStats.attackRange += 0.25f;
    }
    #endregion

}
