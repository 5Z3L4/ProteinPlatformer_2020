using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private float scoreUpdateSpeed;
    [SerializeField] private Text currentScoreText;
    private float displayScore;
    public static int currentScore;
    [SerializeField] private Animator dumbbleAnimator;
    [SerializeField] private Animator meatAnimator;
    [SerializeField] private Animator proteinAnimator;
    public bool callTimer;
    public float time;
    private SaveManager SM;
    public GameObject deathScreen;
    public PlayerMovement player;
    public GameObject dyingBackground;
    public Animation anim;
    //public CinemachineVirtualCamera cam;
    public CinemachineVirtualCamera cam;
    public float cameraSoftZoneHeight;
    public float cameraSoftZoneWidth;
    public float cameraDeadZoneHeight;
    public float cameraDeadZoneWidth;
    private CinemachineFramingTransposer cinemachineBody;
    private Rigidbody2D playerRB;
    private void Awake()
    {
        anim = dyingBackground.GetComponent<Animation>();
        SM = GameObject.FindObjectOfType<SaveManager>();
        player = FindObjectOfType<PlayerMovement>();
    }
    private void Start()
    {
        playerRB = player.GetComponent<Rigidbody2D>();
        cinemachineBody = cam.GetCinemachineComponent<CinemachineFramingTransposer>();
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

    void Update()
    {
        currentScore = GameManager.Score;
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
                GameObject.Find("dumbbelHolder").GetComponent<Animator>().SetBool("Open", false);
                GameObject.Find("meatHolder").GetComponent<Animator>().SetBool("Open", false);
                GameObject.Find("proteinHolder").GetComponent<Animator>().SetBool("Open", false);
            }
        }
    }

    public void ShowCollected(string name)
    {
        GameObject.Find(name).GetComponent<Animator>().SetBool("Close", false);
        GameObject.Find(name).GetComponent<Animator>().SetBool("Open", true);
        callTimer = true;
    }
    public void HideCollected()
    {
        GameObject.Find("dumbbelHolder").GetComponent<Animator>().SetBool("Close", true);
        GameObject.Find("meatHolder").GetComponent<Animator>().SetBool("Close", true);
        GameObject.Find("proteinHolder").GetComponent<Animator>().SetBool("Close", true);
    }
    public void HideHUD()
    {
        GameObject.Find("DumbbelAmountImage").GetComponent<Image>().enabled = false;
        GameObject.Find("DumbbelAmountImage").GetComponentInChildren<Text>().enabled = false;
        GameObject.Find("MeatAmountImage").GetComponent<Image>().enabled = false;
        GameObject.Find("MeatAmountImage").GetComponentInChildren<Text>().enabled = false;
        GameObject.Find("ProteinAmountImage").GetComponent<Image>().enabled = false;
        GameObject.Find("ProteinAmountImage").GetComponentInChildren<Text>().enabled = false;
        GameObject.Find("currentScoreText").GetComponent<Text>().enabled = false;
    }
    public void ShowHUD()
    {
        GameObject.Find("DumbbelAmountImage").GetComponent<Image>().enabled = true;
        GameObject.Find("DumbbelAmountImage").GetComponentInChildren<Text>().enabled = true;
        GameObject.Find("MeatAmountImage").GetComponent<Image>().enabled = true;
        GameObject.Find("MeatAmountImage").GetComponentInChildren<Text>().enabled = true;
        GameObject.Find("ProteinAmountImage").GetComponent<Image>().enabled = true;
        GameObject.Find("ProteinAmountImage").GetComponentInChildren<Text>().enabled = true;
        GameObject.Find("currentScoreText").GetComponent<Text>().enabled = true;
    }
    private IEnumerator ScoreUpdater()
    {
        while (true)
        {
            if (displayScore < currentScore)
            {
                print("dupa");
                displayScore++; //Increment the display score by 1
                currentScoreText.text = "Score: " + displayScore.ToString(); //Write it to the UI
            }
            yield return new WaitForSeconds(scoreUpdateSpeed); // I used .2 secs but you can update it as fast as you want
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
        deathScreen.SetActive(false);
        dyingBackground.SetActive(false); 
        Time.timeScale = 1;
    }
    public IEnumerator DyingScreen()
    {
        //if (player.transform.position.x <= cam.transform.position.x)
        //{
        //    if (player.facingRight)
        //    {
        //        player.transform.position = new Vector3(cam.transform.position.x - 2, cam.transform.position.y, cam.transform.position.z);
        //    }
        //    else
        //    {
        //        player.transform.position = new Vector3(cam.transform.position.x + 2, cam.transform.position.y, cam.transform.position.z);
        //    }
        //}
        //else
        //{
        //    if (player.facingRight)
        //    {
        //        player.transform.position = new Vector3(cam.transform.position.x - 2, cam.transform.position.y, cam.transform.position.z);
        //    }
        //    else
        //    {
        //        player.transform.position = new Vector3(cam.transform.position.x + 2, cam.transform.position.y, cam.transform.position.z);
        //    }
        //}
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
        yield return new WaitForSeconds(1.5f);
        deathScreen.SetActive(true);
        Time.timeScale = 0;
    }

}
