using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ChangeScene : MonoBehaviour
{
    public SaveManager saveManager;
    public TMP_Text errorText;
    public Animator loadingAnim;
    private bool _canLoadMenu = false;
    public void OnClick(string levelName)
    {
        StartCoroutine(LoadLevel(levelName));
    }
    private IEnumerator LoadLevel(string levelName)
    {
        loadingAnim.SetBool("IsLoading", true);
        yield return new WaitForSeconds(3f);
        if (saveManager.idToken == string.Empty)
        {
            errorText.SetText("Login or password is incorrect");
        }
        loadingAnim.SetBool("IsLoading", false);
    }

    private void Update()
    {
        if (saveManager.playerName != string.Empty)
        {
            SceneManager.LoadScene("Menu 1");
        }
    }
}
