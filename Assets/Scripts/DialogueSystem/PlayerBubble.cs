using UnityEngine;
using TMPro;

public class PlayerBubble : MonoBehaviour
{
    private SpriteRenderer backgroundSpriteRenderer;
    private TextMeshPro textMeshPro;
    public DialogueObject playerBubbleText;
    private void Awake()
    { 
        backgroundSpriteRenderer = transform.Find("BubbleBackground").GetComponent<SpriteRenderer>();
        textMeshPro = backgroundSpriteRenderer.transform.GetComponentInChildren<TextMeshPro>();
    }
    public void SetSizeOfTextBackground(DialogueObject newPlayerBubbleText)
    {
        textMeshPro.SetText(newPlayerBubbleText.Dialogue[0]);
        textMeshPro.ForceMeshUpdate();
        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        backgroundSpriteRenderer.size = textSize + new Vector2(1f, 0.5f);
    }

    public void SetSizeOfTextBackground(string text)
    {
        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate();
        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        backgroundSpriteRenderer.size = textSize + new Vector2(1f, 0.5f);
    }
}

