using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TakeMeToAnotherScene : MonoBehaviour
{
    public Animator SceneTransition;
    public TimeCounter timer;
    private bool _isPlayerInTrigger = false;
    private void Awake()
    {
        timer = FindObjectOfType<TimeCounter>();
    }
    private void Update()
    {
        if (!_isPlayerInTrigger) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(LoadBossScene());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isPlayerInTrigger = true;
        }
    }
    private IEnumerator LoadBossScene()
    {
        timer.AddPointsForTime();
        SceneTransition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("BossLevel1 muscular");
    }
}
