using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collect : MonoBehaviour
{
    private Animation anim;
    private TMP_Text text;
    private Collectible scoreValue;
    private void Awake()
    {
        anim = GetComponent<Animation>();
        text = GetComponentInChildren<TMP_Text>();
        scoreValue = GetComponent<Collectible>();
    }
    public void CollectItem()
    {
        text.SetText("+" + scoreValue.scoreValue.ToString());
        anim.Play("CollectItemAnimation");
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        Destroy(gameObject, anim.GetClip("CollectItemAnimation").length);
    }
}
