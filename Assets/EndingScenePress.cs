using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScenePress : MonoBehaviour
{
    private bool _shouldCheckForPress = false;
    void Update()
    {
        if (_shouldCheckForPress)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _shouldCheckForPress = false;
                SceneManager.LoadScene("Menu 1");
            }
        }
    }
    private void OnEnable()
    {
        _shouldCheckForPress = true;
    }
}
