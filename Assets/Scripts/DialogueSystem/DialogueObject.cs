using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    public OnQuestComplete tutorial;
    [SerializeField] [TextArea] private string[] dialogue;
    [SerializeField] private Response[] responses;
    public string[] Dialogue => dialogue;
    public Response[] Responses => responses;
    public bool HasResponses => Responses != null && Responses.Length > 0;
    public void SetBool()
    {
        tutorial.dialogueFinished = true;
    }
}
