using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowKarkScore : MonoBehaviour
{
    private Kark kark;
    [SerializeField] private Animation anim;
    [SerializeField] private TMP_Text scoreText;
    private void Start()
    {
        ShowScoreText();
    }
    private void Awake()
    {
        kark = FindObjectOfType<Kark>().GetComponent<Kark>();
        anim = GetComponent<Animation>();
    }
    void ShowScoreText()
    {
        scoreText.SetText("+" + kark.scoreValue.ToString());
        anim.Play("karkScoreAnim");
        Destroy(gameObject.transform.parent.gameObject, anim.GetClip("karkScoreAnim").length);
    }
}
