using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBubble : MonoBehaviour
{
    private TextWriter textWriter;
    private SpriteRenderer backgroundSpriteRenderer;
    private TextMeshPro textMeshPro;

    private string[] bubbleTexts = new string[5] { "test1", "test2", "test3", "test4", "test5" };

    private void Awake()
    {
        textWriter = GetComponent<TextWriter>();
        backgroundSpriteRenderer = transform.Find("BubbleBackground").GetComponent<SpriteRenderer>();
        textMeshPro = backgroundSpriteRenderer.transform.GetComponentInChildren<TextMeshPro>();
    }
    public void BubbleSetup()
    {
        string randomText = bubbleTexts.GetValue(Random.Range(0, bubbleTexts.Length)).ToString();
        textMeshPro.SetText(randomText);
        textMeshPro.ForceMeshUpdate();
        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        backgroundSpriteRenderer.size = textSize + new Vector2(1f, 1f);
    }
}
