using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collect : MonoBehaviour
{
    private Animation anim;
    private TMP_Text text;
    private Collectible scoreValue;
    [SerializeField] private string popUpText;
    private void Awake()
    {
        anim = GetComponent<Animation>();
        text = GetComponentInChildren<TMP_Text>();
        scoreValue = GetComponent<Collectible>();
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
        anim.Play("CollectItemAnimation");
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        Destroy(gameObject, anim.GetClip("CollectItemAnimation").length);
    }
}
