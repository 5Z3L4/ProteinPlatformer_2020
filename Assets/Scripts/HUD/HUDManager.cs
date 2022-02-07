using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    
    public Animator animatorDumbbell, animatorMeat, animatorProtein;
    public static int currentScore;
    public bool callTimer;
    public float time;
    public Canvas canvas;
    public Canvas deathScreenCanvas;
    public PlayerMovement player;
    public GameObject dyingBackground;
    public Animation anim;
    //public CinemachineVirtualCamera cam;
    public CinemachineVirtualCamera cam;
    public float cameraSoftZoneHeight;
    public float cameraSoftZoneWidth;
    public float cameraDeadZoneHeight;
    public float cameraDeadZoneWidth;
    public GameController GameController;
    private CinemachineFramingTransposer cinemachineBody;
    private Rigidbody2D playerRB;
    private Text dumbbelText, proteinText, meatText;
    private Image dumbbelImage, meatImage, proteinImage;
    private Text scoreText;
    private SaveManager SM;
    [SerializeField] private TMP_Text hpAmountText;
    [SerializeField] private float scoreUpdateSpeed;
    [SerializeField] private Text currentScoreText;
    [SerializeField] private Text specificItemAmount;
    [SerializeField] private GameObject specificItemImage;
    private float displayScore;
    
    private void Awake()
    {
        anim = dyingBackground.GetComponent<Animation>();
        SM = GameObject.FindObjectOfType<SaveManager>();
        player = FindObjectOfType<PlayerMovement>();
        playerRB = player.GetComponent<Rigidbody2D>();
        cinemachineBody = cam.GetCinemachineComponent<CinemachineFramingTransposer>();
        dumbbelImage = GameObject.Find("DumbbelAmountImage").GetComponentInChildren<Image>();
        dumbbelText = GameObject.Find("DumbbelAmountImage").GetComponentInChildren<Text>();
        meatImage = GameObject.Find("MeatAmountImage").GetComponent<Image>();
        meatText = GameObject.Find("MeatAmountImage").GetComponentInChildren<Text>();
        proteinImage = GameObject.Find("ProteinAmountImage").GetComponent<Image>();
        proteinText = GameObject.Find("ProteinAmountImage").GetComponentInChildren<Text>();
        scoreText = GameObject.Find("currentScoreText").GetComponent<Text>();

    }
    private void Start()
    {
        if (GameManager.specificLevelItemOnMap == 0)
        {
            specificItemImage.SetActive(false);
        }
        hpAmountText.SetText("x " + player.hp.ToString());
        cameraSoftZoneHeight = cinemachineBody.m_SoftZoneHeight;
        cameraSoftZoneWidth = cinemachineBody.m_SoftZoneWidth;
        cameraDeadZoneHeight = cinemachineBody.m_DeadZoneHeight;
        cameraDeadZoneWidth = cinemachineBody.m_DeadZoneWidth;

        if (scoreUpdateSpeed == 0)
        {
            scoreUpdateSpeed = 0.2f;
        }
        currentScore = GameManager.Score;
        displayScore = 0;
        callTimer = false;
        time = 3.5f;
        currentScoreText.text = "Score: " + displayScore.ToString();
        StartCoroutine(ScoreUpdater());
    }

    //TO DO poprawiæ
    void Update()
    {
        if (specificItemImage.activeInHierarchy)
        {
            specificItemAmount.text = GameManager.collectedSpecificItems.ToString() + "/" + GameManager.specificLevelItemOnMap.ToString();
        }
        currentScore = GameManager.Score;
        hpAmountText.SetText("x " + player.hp.ToString());
        if (callTimer && GameManager.isStoryMode)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            else if (time <= 0)
            {
                HideCollected();
                callTimer = false;
                time = 3.5f;
                animatorDumbbell.SetBool("Open", false);
                animatorMeat.SetBool("Open", false);
                animatorProtein.SetBool("Open", false);
            }
        }
    }
    //TO DO poprawiæ
    public void ShowCollected(Animator animator)
    {
        animator.SetBool("Close", false);
        animator.SetBool("Open", true);
        callTimer = true;
    }
    //TO DO poprawiæ
    public void HideCollected()
    {
        animatorDumbbell.SetBool("Close", true);
        animatorMeat.SetBool("Close", true);
        animatorProtein.SetBool("Close", true);
    }
    public void HideHUD()
    {
        ChangeHUDVisibility(false);
    }
    public void ShowHUD()
    {
        ChangeHUDVisibility(true);
    }

    public void ChangeHUDVisibility(bool shouldBeVisible)
    {
        dumbbelImage.enabled = shouldBeVisible;
        dumbbelText.enabled = shouldBeVisible;
        meatImage.enabled = shouldBeVisible;
        meatText.enabled = shouldBeVisible;
        proteinImage.enabled = shouldBeVisible;
        proteinText.enabled = shouldBeVisible;
        scoreText.enabled = shouldBeVisible;
    }
    private IEnumerator ScoreUpdater()
    {
        while (true)
        {
            if (displayScore < currentScore)
            {
                displayScore += 5; //Increment the display score by 5
                currentScoreText.text = "Score: " + displayScore.ToString(); //Write it to the UI
            }
            yield return new WaitForSeconds(scoreUpdateSpeed);
        }
    }
    public void Respawn()
    {
        player.Respawn();
        playerRB.constraints = RigidbodyConstraints2D.None;
        playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        cinemachineBody.m_SoftZoneHeight = cameraSoftZoneHeight;
        cinemachineBody.m_SoftZoneWidth = cameraSoftZoneWidth;
        cinemachineBody.m_DeadZoneHeight = cameraDeadZoneHeight;
        cinemachineBody.m_DeadZoneWidth = cameraDeadZoneWidth;
        cinemachineBody.m_ScreenX = 0.5f;
        deathScreenCanvas.gameObject.SetActive(false);
        canvas.gameObject.SetActive(true);
        dyingBackground.SetActive(false);
        GameController.RespawnPlatforms();
        Time.timeScale = 1;
    }
    public IEnumerator DyingScreen()
    {
        SFXManager.PlaySound(SFXManager.Sound.Fail, transform.position);
        cinemachineBody.m_SoftZoneHeight = 0;
        cinemachineBody.m_SoftZoneWidth = 0;
        if (player.facingRight)
        {
            cinemachineBody.m_ScreenX -= 0.04f;
        }
        else
        {
            cinemachineBody.m_ScreenX += 0.04f;
        }
        playerRB.constraints = RigidbodyConstraints2D.FreezePositionY;
        playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        dyingBackground.SetActive(true);
        anim.Play("dyingBackgroundAnimation");
        canvas.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        deathScreenCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

}
