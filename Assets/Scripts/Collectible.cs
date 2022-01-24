using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public HUDManager HUDM;
    public SaveManager SM;
    private Collect collectItem;
    private bool isCollected = false;
    public Animator holder;
    private void Awake()
    {
        collectItem = GetComponent<Collect>();
    }
    //enum statystyk
    public enum StatsToUpgrade
    {
        Strength,
        Constitution,
        Dexterity,
        None
    }

    public StatsToUpgrade stats;
    public int scoreValue;

    private void Start()
    {
        SM = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>();
        HUDM = GameObject.Find("HUDManager").GetComponent<HUDManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isCollected) return;
        if (collision.CompareTag("Player"))
        {
            isCollected = true;
            SFXManager.PlaySound(SFXManager.Sound.PickUpItem, gameObject.transform.position);
            
            if (stats == StatsToUpgrade.Strength)
            {
                GameManager.collectedStrenght++;
                ShowCollectibleUI(holder);
            }
            else if (stats == StatsToUpgrade.Constitution)
            {
                GameManager.collectedConstitution++;
                ShowCollectibleUI(holder);
            }
            else if (stats == StatsToUpgrade.Dexterity)
            {
                GameManager.collectedAgility++;
                ShowCollectibleUI(holder);
            }
            else if (stats == StatsToUpgrade.None)
            {
                return;
            }
            GameManager.Score += scoreValue;
            HUDManager.currentScore += scoreValue;
            SM.levels[SM.currentLevelId].score += scoreValue;
            collectItem.CollectItem();
        }
    }

    private void ShowCollectibleUI(Animator holder)
    {
        if (holder != null && !holder.GetBool("Open"))
        {
            HUDM.ShowCollected(holder);
            HUDM.callTimer = true;
            HUDM.time += 0.5f;
        }
    }
}
