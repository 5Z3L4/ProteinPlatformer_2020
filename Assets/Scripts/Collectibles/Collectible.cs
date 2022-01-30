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
    public StatsToUpgrade stats;
    public int scoreValue;
    public bool shouldBeCounted;
    //enum statystyk prob do usuniêcia
    public enum StatsToUpgrade
    {
        Strength,
        Constitution,
        Dexterity,
        None
    }

    private void Awake()
    {
        collectItem = GetComponent<Collect>();
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
            }
            else if (stats == StatsToUpgrade.Constitution)
            {
                GameManager.collectedConstitution++;
            }
            else if (stats == StatsToUpgrade.Dexterity)
            {
                GameManager.collectedAgility++;
            }
            else if (stats == StatsToUpgrade.None)
            {
                return;
            }
            ShowCollectibleUI(holder);
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
