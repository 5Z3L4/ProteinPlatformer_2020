
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collect : MonoBehaviour
{
    public GameObject MinimapIcon;
    private Animation anim;
    private TMP_Text text;
    private Collectible scoreValue;
    private SpriteRenderer collSprite;
    private CircleCollider2D myColl;
    [SerializeField] private string popUpText;
    private void Awake()
    {
        collSprite = GetComponentInChildren<SpriteRenderer>();
        if (GetComponent<Animation>() != null)
        {
            anim = GetComponent<Animation>();
        }
        text = GetComponentInChildren<TMP_Text>();
        scoreValue = GetComponent<Collectible>();
        myColl = GetComponent<CircleCollider2D>();
    }
    public void CollectItem()
    {
        if (string.IsNullOrEmpty(popUpText))
        {
            text.SetText("+" + scoreValue.scoreValue.ToString());
        }
        else
        {
            text.SetText(popUpText);
        }
        if (anim!= null)
        {
            anim.Play("ShowCollectText");
        }
        if (gameObject.CompareTag("JumpBoost"))
        {
            StartCoroutine(BoostOnCd());
        }
        else
        {
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            if (MinimapIcon != null)
            {
                MinimapIcon.SetActive(false);
            }
            Destroy(gameObject, anim.GetClip("ShowCollectText").length);
        }
        
    }

    public IEnumerator BoostOnCd()
    {
        myColl.enabled = false;
        collSprite.color = new Color(1, 1, 1, 0.3f);
        yield return new WaitForSeconds(5f);
        collSprite.color = new Color(1, 1, 1, 1);
        myColl.enabled = true;
    }
}
