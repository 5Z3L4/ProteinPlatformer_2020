using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public HUDManager HUDM;
    public SaveManager SM;
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
        SM = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>();
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
            SFXManager.PlaySound(SFXManager.Sound.PickUpItem, gameObject.transform.position);
            if (stats == StatsToUpgrade.Strength)
            {
                GameManager.collectedStrenght++;
                ShowCollectibleUI("dumbbelHolder");
            }
            else if (stats == StatsToUpgrade.Constitution)
            {
                GameManager.collectedConstitution++;
                ShowCollectibleUI("meatHolder");
            }
            else if (stats == StatsToUpgrade.Dexterity)
            {
                GameManager.collectedAgility++;
                ShowCollectibleUI("proteinHolder");
            }
            HUDManager.currentScore += scoreValue;
            SM.level1.score += scoreValue;
            gameObject.SetActive(false);
        }
    }

    private void ShowCollectibleUI(string name)
    {
        if (!GameObject.Find(name).GetComponent<Animator>().GetBool("Open"))
        {
            HUDM.ShowCollected(name);
            HUDM.callTimer = true;
            HUDM.time += 0.5f;
        }
    }
}
