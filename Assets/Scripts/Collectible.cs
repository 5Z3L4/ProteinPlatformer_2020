using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public GameManager GM;
    private Vector3 startPosition;
    private float maxSpeed = 3f;

    //enum statystyk
    public enum StatsToUpgrade { 
        Strength,
        Constitution,
        Dexterity
    }

    public StatsToUpgrade stats;
    public int scoreValue;

    private void Start()
    {
        startPosition = transform.position;
        GM = FindObjectOfType<GameManager>();
    }
    //private void Update()
    //{
    //    transform.position = new Vector3(transform.position.x, startPosition.y + (Mathf.Sin(Time.time * maxSpeed))/4, transform.position.y);
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (stats == StatsToUpgrade.Strength)
            {
                GM.CollectedStrenght++;
                HUDManager.currentScore += scoreValue;
                CollectiblesAmount.dumbbleAmount++;
            }
            else if(stats == StatsToUpgrade.Constitution)
            {
                GM.CollectedConstitution++;
                HUDManager.currentScore += scoreValue;
                CollectiblesAmount.meatAmount++;
            }
            else if (stats == StatsToUpgrade.Dexterity)
            {
                GM.CollectedAgility++;
                HUDManager.currentScore += scoreValue;
                CollectiblesAmount.proteinAmount++;
            }
            gameObject.SetActive(false);
        }
    }

 

    
}
