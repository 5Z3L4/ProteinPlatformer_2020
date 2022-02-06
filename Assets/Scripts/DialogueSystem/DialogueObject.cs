using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] private bool isQuestDialogue;
    [SerializeField] [TextArea] private string[] dialogue;
    [SerializeField] private Response[] responses;
    public string[] Dialogue => dialogue;
    public Response[] Responses => responses;
    public bool IsQuestDialogue => isQuestDialogue;
    public bool HasResponses => Responses != null && Responses.Length > 0;
}
