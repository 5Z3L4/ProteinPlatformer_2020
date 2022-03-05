using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObjActAnimator : StateMachineBehaviour
{
    //wrong naming, it's USING METHOD TO SET ACTIVE FROM SELECTED OBJECT (Set Active Obj script)
    public string GameObjName;
    private SetActiveObj _ojbToSetActive;

    private void Awake()
    {
        _ojbToSetActive = GameObject.Find(GameObjName).GetComponent<SetActiveObj>();
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _ojbToSetActive.ActivateObj();
    }
}
