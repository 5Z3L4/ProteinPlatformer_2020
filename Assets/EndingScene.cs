using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScene : MonoBehaviour
{
    public Canvas[] canvasesToDisable;
    public GameObject endingScene;
    public PlayerResponses lastPlayerRes;
    private DialogueActivator _dialogue;
    private Animator _anim;
    private bool _shouldChangeAnim = true;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _dialogue = GetComponent<DialogueActivator>();
    }

    private void OnEnable()
    {
        _anim.SetBool("Start", true);
    }

    void Update()
    {
        if (_shouldChangeAnim)
        {
            if (lastPlayerRes.isOver)
            {
                endingScene.SetActive(true);
                if (canvasesToDisable.Length > 0)
                {
                    foreach (Canvas canvasToDisable in canvasesToDisable)
                    {
                        if (canvasesToDisable != null)
                        {
                            canvasToDisable.enabled = false;
                        }
                    }
                }
            }
        }
    }
    public void EnableDialogue()
    {
        _dialogue.enabled = true;
    }
}
