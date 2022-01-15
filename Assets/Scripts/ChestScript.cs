using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    public bool isOpen = false;
    public int goodLootChance;
    public int scoreAmount;
    private bool isPlayerThere = false;
    public GameObject brokenChest;
    public PlayerMovement player;
    SpriteRenderer sprite;
    private void Awake()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerMovement>();
    }
    private void Update()
    {
        if (isPlayerThere)
        {
            if (!isOpen)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    OpenBox();
                }
            }
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && player.isSliding)
        {
            BrokeChest();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
         isPlayerThere = true;
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
        SFXManager.PlaySound(SFXManager.Sound.DestroyChest, transform.position);
        brokenChest.SetActive(true);
        sprite.enabled = false;
    }
}
