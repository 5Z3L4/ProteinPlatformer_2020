using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPages : MonoBehaviour
{
    public Canvas[] canvases;
    public GameObject[] tutorialPages;

    private PlayerMovement _player;
    private int _pageID = 0;
    private bool _isTutorialOpen = false;
    private float _jumpBuffer;
    private void Awake()
    {
        _player = FindObjectOfType<PlayerMovement>();
        _jumpBuffer = _player.jumpBuffer;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isTutorialOpen) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            DeactivateTutorialPage(_pageID);
        }
    }

    public void ActivateTutorialPage(int pageID)
    {
        _player.canMove = false;
        _isTutorialOpen = true;
        _pageID = pageID;
        tutorialPages[_pageID].SetActive(true);
        foreach (Canvas canvas in canvases)
        {
            if (canvas != null)
            {
                canvas.gameObject.SetActive(false);
            }
        }
    }
    public void DeactivateTutorialPage(int pageID)
    {
        _player.canMove = true;
        _isTutorialOpen = false;
        foreach (Canvas canvas in canvases)
        {
            if (canvas != null)
            {
                if (!canvas.gameObject.activeInHierarchy)
                {
                    canvas.gameObject.SetActive(true);
                }
            }
        }
        if (tutorialPages[_pageID] != null)
        {
            tutorialPages[_pageID].SetActive(false);
        }
        if (pageID == 1)
        {
            _player.jumpBuffer = _jumpBuffer;
        }
    }
}
