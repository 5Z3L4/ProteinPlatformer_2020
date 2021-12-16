using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;
    private TextWriter textWriter;
    public bool IsOpen { get; private set; }

    void Start()
    {
        CloseDialogueBox();
        textWriter = GetComponent<TextWriter>();
    }
    public void ShowDialogue(DialogueObject dialogueObject)
    {
        IsOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }
    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];
            yield return RunTypingEffect(dialogue);
            textLabel.text = dialogue;
            if (i == dialogueObject.Dialogue.Length - 1)
            {
                break;
            }
            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
        }
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
        CloseDialogueBox();
    }
    private void CloseDialogueBox()
    {
        IsOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
    private IEnumerator RunTypingEffect(string dialogue)
    {
        textWriter.Run(dialogue, textLabel);
        while (textWriter.IsRunning)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.R))
            {
                textWriter.Stop();
            }
        }
    }
}
