using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Pause : MonoBehaviour
{
    public GameObject firstSelectedButton;
    public GameObject firstSelectedOnConfirm;
    public GameObject confirmPanel;

    private PlayerMovement player;
    private float jumpBufferTemp = 0;
    private bool gamePaused = false;
    [SerializeField] private GameObject pauseScreen;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
    }
    private void Start()
    {
        jumpBufferTemp = player.jumpBuffer;
    }
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
                StartCoroutine(ResetJumpBuffer(1f));

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
                player.jumpBuffer = 0;
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
            StartCoroutine(ResetJumpBuffer(1f));
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
    private IEnumerator ResetJumpBuffer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        player.jumpBuffer = jumpBufferTemp;
    }
}
