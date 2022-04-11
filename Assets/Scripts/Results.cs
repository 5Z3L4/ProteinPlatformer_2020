using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class Results : MonoBehaviour
{
    public Animator sceneTransition;
    public GameObject firstSelected;
    public TMP_Text scoreResult;

    private PlayerMovement player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    private void OnEnable()
    {
        player.canMove = false;
        EventSystem.current.firstSelectedGameObject = firstSelected;
        EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        scoreResult.SetText("Score: " + GameManager.Score.ToString());
    }
    public void NextLevel(string levelName)
    {
        StartCoroutine(SceneTransition());
        SceneManager.LoadScene(levelName);
        GameManager.Reset();
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    private IEnumerator SceneTransition()
    {
        sceneTransition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
    }
        
}
