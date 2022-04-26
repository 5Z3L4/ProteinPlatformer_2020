using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public SaveManager saveManager;
    public void OnClick(string levelName)
    {
        StartCoroutine(LoadLevel(levelName));
    }
    private IEnumerator LoadLevel(string levelName)
    {
        yield return new WaitForSeconds(1f);
        if (saveManager.idToken != string.Empty)
        {

        }
        else
        {
            Debug.Log("Login and password doesn't match");
        }
    }

    private void Update()
    {
        if (saveManager.idToken != string.Empty)
        {
            SceneManager.LoadScene("Menu 1");
        }
    }
}
