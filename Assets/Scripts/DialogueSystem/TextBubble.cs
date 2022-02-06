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
    public bool shouldDisplayNegativeTexts = false;

    private string[] bubbleTextsNegative = new string[5] { "Disgusting...", "Freak!", "Get away!", "Worm!", "Ugh, something stinks..." };
    private string[] bubbleTextsPositive = new string[5] { "Mmmm...", "Hey honey!", "Nice chest!", "Woow!", "Damn, you're so sexy..." };
    private void Awake()
    {
        backgroundSpriteRenderer = transform.Find("BubbleBackground").GetComponent<SpriteRenderer>();
        textMeshPro = backgroundSpriteRenderer.transform.GetComponentInChildren<TextMeshPro>();
        if (startingDialogue == null)
        {
            startingDialogueFinished = true;
        }
        else
        {
            startingDialogueFinished = false;
        }
    }
    public void BubbleSetup()
    {
        if (startingDialogueFinished)
        {
            string randomText;
            if (!shouldDisplayNegativeTexts)
            {
                randomText = bubbleTextsPositive.GetValue(Random.Range(0, bubbleTextsNegative.Length)).ToString();
            }
            else
            {
                randomText = bubbleTextsNegative.GetValue(Random.Range(0, bubbleTextsNegative.Length)).ToString();
            }
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
