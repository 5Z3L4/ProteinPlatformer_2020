using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveObj : MonoBehaviour
{
    public GameObject ObjToActive;

    public void ActivateObj()
    {
        ObjToActive.SetActive(true);
    }
}
