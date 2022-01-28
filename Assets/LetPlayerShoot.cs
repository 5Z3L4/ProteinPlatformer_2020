using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetPlayerShoot : MonoBehaviour
{
    public ThrowDumbbel playerShot;
    private void OnEnable()
    {
        playerShot.canIShoot = true;
    }
}
