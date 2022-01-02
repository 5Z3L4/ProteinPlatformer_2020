using UnityEngine;
[System.Serializable]
public class Response
{
    [TextArea(1,3)][SerializeField] private string responseText;
    [SerializeField] private DialogueObject dialogueObject;
    public string ResponseText => responseText;
    public DialogueObject DialogueObject => dialogueObject;
}
