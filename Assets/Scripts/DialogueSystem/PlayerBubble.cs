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
    public void BubbleSetup(DialogueObject newPlayerBubbleText)
    {
        textMeshPro.SetText(newPlayerBubbleText.Dialogue[0]);
        textMeshPro.ForceMeshUpdate();
        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        backgroundSpriteRenderer.size = textSize + new Vector2(1f, 1f);
    }
}

