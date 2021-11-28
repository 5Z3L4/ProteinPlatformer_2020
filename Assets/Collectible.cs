using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    //enum statystyk
    public enum StatsToUpgrade { 
        Strength,
        Constitution,
        Dexterity
    }

    public StatsToUpgrade stats;
    public int scoreValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (stats == StatsToUpgrade.Strength)
            {
                PlayerStats.attackRange++;
            }
            else if(stats == StatsToUpgrade.Constitution)
            {
                PlayerStats.moveSpeed++;
            }
            else if (stats == StatsToUpgrade.Dexterity)
            {
                //
            }
            Destroy(gameObject);
        }
    }
}
