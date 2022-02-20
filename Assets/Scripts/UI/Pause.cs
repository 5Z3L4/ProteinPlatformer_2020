using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject firstSelectedButton;
    public GameObject firstSelectedOnConfirm;
    public GameObject confirmPanel;

    private bool gamePaused = false;
    [SerializeField] private GameObject pauseScreen;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                if (confirmPanel.activeInHierarchy)
                {
                    confirmPanel.SetActive(false);
                }
                else
                {
                    pauseScreen.gameObject.SetActive(false);
                    gamePaused = false;
                    Time.timeScale = 1;
                    EventSystem.current.firstSelectedGameObject = null;
                    EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
                }
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                pauseScreen.gameObject.SetActive(true);
                gamePaused = true;
                Time.timeScale = 0;
                EventSystem.current.firstSelectedGameObject = firstSelectedButton;
                EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            
        }
    }
    public void Resume()
    {
        if (gamePaused)
        {
            pauseScreen.gameObject.SetActive(false);
            gamePaused = false;
            Time.timeScale = 1;
            EventSystem.current.firstSelectedGameObject = null;
            EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public void ShowConfirmPanel()
    {
        confirmPanel.SetActive(true);
        EventSystem.current.firstSelectedGameObject = null;
        EventSystem.current.firstSelectedGameObject = firstSelectedOnConfirm;
        EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
    }
    public void BackToPauseMenu()
    {
        confirmPanel.SetActive(false);
        EventSystem.current.firstSelectedGameObject = null;
        EventSystem.current.firstSelectedGameObject = firstSelectedButton;
        EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
}
