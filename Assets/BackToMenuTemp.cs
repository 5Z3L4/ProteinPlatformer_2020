using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenuTemp : MonoBehaviour
{
    private Animator fade;
    private void Awake()
    {
        fade = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartFade());
    }
    private IEnumerator StartFade()
    {
        yield return new WaitForSeconds(4f);
        fade.SetBool("Start", true);
        yield return new WaitForSeconds(fade.GetCurrentAnimatorStateInfo(0).length);
        SceneManager.LoadScene("Menu 1");
    }
}
