using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefCaseController : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("x"))
        {
            print("x");
            Destroy(gameObject);
        }
    }
}
