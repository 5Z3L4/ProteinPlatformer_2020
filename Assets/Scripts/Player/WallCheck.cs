using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    public GameObject collider;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            collider.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collider.SetActive(false);
    }
}
