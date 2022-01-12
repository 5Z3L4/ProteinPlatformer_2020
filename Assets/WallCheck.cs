using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    public GameObject collider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.layer.ToString());
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
