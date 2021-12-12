using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public HUDManager HUDM;

    //enum statystyk
    public enum StatsToUpgrade
    {
        Strength,
        Constitution,
        Dexterity
    }

    public StatsToUpgrade stats;
    public int scoreValue;

    private void Start()
    {
        if (stats == StatsToUpgrade.Dexterity)
        {
            GameManager.maxAgility++;
        }
        else if (stats == StatsToUpgrade.Strength)
        {
            GameManager.maxStrenght++;
        }
        else if (stats == StatsToUpgrade.Constitution)
        {
            GameManager.maxConstitution++;
        }

        HUDM = GameObject.Find("HUDManager").GetComponent<HUDManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (stats == StatsToUpgrade.Strength)
            {
                GameManager.collectedStrenght++;
                HUDManager.currentScore += scoreValue;
                if (!GameObject.Find("dumbbelHolder").GetComponent<Animator>().GetBool("Open"))
                {
                    HUDM.ShowCollected("dumbbelHolder");
                    HUDM.callTimer = true;
                    HUDM.time += 0.5f;
                }
            }
            else if (stats == StatsToUpgrade.Constitution)
            {
                GameManager.collectedConstitution++;
                HUDManager.currentScore += scoreValue;
                if (!GameObject.Find("meatHolder").GetComponent<Animator>().GetBool("Open"))
                {
                    HUDM.ShowCollected("meatHolder");
                    HUDM.callTimer = true;
                    HUDM.time += 0.5f;
                }
            }
            else if (stats == StatsToUpgrade.Dexterity)
            {
                GameManager.collectedAgility++;
                HUDManager.currentScore += scoreValue;
                if (!GameObject.Find("proteinHolder").GetComponent<Animator>().GetBool("Open"))
                {
                    HUDM.ShowCollected("proteinHolder");
                    HUDM.callTimer = true;
                    HUDM.time += 0.5f;
                }
            }
            gameObject.SetActive(false);
        }
    }
}
