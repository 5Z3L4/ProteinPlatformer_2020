using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TakeToBoss : MonoBehaviour
{
    bool takeToBoss = false;
    private void Update()
    {
        if (!takeToBoss) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("BossLevel1");
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            takeToBoss = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            takeToBoss = false;
        }
    }
}
