using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    public bool isOpen = false;
    public int goodLootChance;
    public int scoreAmount;
    public GameObject brokenChest;
    public PlayerMovement player;
    SpriteRenderer sprite;
    private void Awake()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && (player.isSliding || player.isCharging))
        {
            BrokeChest();
        }
    }

    public void OpenBox()
    {
            isOpen = true;
            if (Random.Range(0, 100) <= goodLootChance)
            {
                GameManager.Score += scoreAmount;
                print("uda³o siê zwiêkszyæ punkty ez" + GameManager.Score);
            }
            else
            {
                print("No nie pyk³o");
                //zabierz hp
            }
        
    }

    [ContextMenu("DestroyChest")]
    public void BrokeChest()
    {
        GameManager.collectedChests++;
        SFXManager.PlaySound(SFXManager.Sound.DestroyChest, transform.position);
        brokenChest.SetActive(true);
        sprite.enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
