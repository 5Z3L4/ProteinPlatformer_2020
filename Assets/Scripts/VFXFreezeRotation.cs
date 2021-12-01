using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXFreezeRotation : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}