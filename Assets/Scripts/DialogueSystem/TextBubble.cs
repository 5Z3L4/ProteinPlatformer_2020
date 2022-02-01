using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBubble : MonoBehaviour
{
    private SpriteRenderer backgroundSpriteRenderer;
    private TextMeshPro textMeshPro;
    [SerializeField] private DialogueObject startingDialogue;
    public bool startingDialogueFinished;

    private string[] bubbleTexts = new string[5] { "Disgusting...", "Freak!", "Get away!", "Worm!", "Ugh, something stinks..." };
    private void Awake()
    {
        if (startingDialogue == null)
        {
            startingDialogueFinished = true;
        }
        else
        {
            startingDialogueFinished = false;
        }
        backgroundSpriteRenderer = transform.Find("BubbleBackground").GetComponent<SpriteRenderer>();
        textMeshPro = backgroundSpriteRenderer.transform.GetComponentInChildren<TextMeshPro>();
    }
    public void BubbleSetup()
    {
        if (startingDialogueFinished)
        {
            string randomText = bubbleTexts.GetValue(Random.Range(0, bubbleTexts.Length)).ToString();
            textMeshPro.SetText(randomText);
            textMeshPro.ForceMeshUpdate();
            Vector2 textSize = textMeshPro.GetRenderedValues(false);
            backgroundSpriteRenderer.size = textSize + new Vector2(1f, 0.5f);
        }
        else
        {
            textMeshPro.SetText(startingDialogue.Dialogue[0]);
            textMeshPro.ForceMeshUpdate();
            Vector2 textSize = textMeshPro.GetRenderedValues(false);
            backgroundSpriteRenderer.size = textSize + new Vector2(1f, 0.5f);
        }
    }
}
