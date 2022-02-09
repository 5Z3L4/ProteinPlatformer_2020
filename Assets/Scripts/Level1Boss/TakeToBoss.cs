using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TakeToBoss : MonoBehaviour
{
    public Animator SceneTransition;
    private bool _takeToBoss = false;
    public TimeCounter timer;
    private void Awake()
    {
        timer = FindObjectOfType<TimeCounter>();
    }
    private void Update()
    {
        if (!_takeToBoss) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(LoadBossScene());
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _takeToBoss = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _takeToBoss = false;
        }
    }

    private IEnumerator LoadBossScene()
    {
        timer.AddPointsForTime();
        SceneTransition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("BossLevel1");
    }
}
