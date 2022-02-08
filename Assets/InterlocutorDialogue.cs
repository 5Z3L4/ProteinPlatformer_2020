using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterlocutorDialogue : MonoBehaviour
{ 
    public bool shouldCheckForEnd;
    public bool isOver;
    [TextArea] public string[] interlocutorDialogues;
    public PlayerResponses[] playerResponse;
    public bool HasResponses => playerResponse != null && playerResponse.Length > 0;
}
