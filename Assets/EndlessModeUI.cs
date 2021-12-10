using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndlessModeUI : MonoBehaviour
{
    public HUDManager HUDM;
    public EndlessGameManager theGameManager;
    public Text Score;
    public Text DumbbleCollected;
    public Text MeatCollected;
    public Text ProteinCollected;
    public GameObject DeathScreen;
    public static int dumbbleAmount;
    public static int meatAmount;
    public static int proteinAmount;
    public static int score;

    private void Update()
    {
        
    }
    public void RestartButton()
    {
        HUDM.ShowHUD();
        HUDManager.currentScore = 0;
        GameManager.collectedStrenght = 0;
        GameManager.collectedConstitution = 0;
        GameManager.collectedAgility = 0;
        Time.timeScale = 1;
        DeathScreen.SetActive(false);
        theGameManager.RestartGame();
        
    }
    public void ShowDeathScreen()
    {
        HUDM.HideHUD();
        DeathScreen.SetActive(true);
        Time.timeScale = 0;
        Score.text = "Your score: " + HUDManager.currentScore.ToString();
        DumbbleCollected.text = "Dumbbles: " + GameManager.collectedStrenght.ToString();
        MeatCollected.text = "Meat: " + GameManager.collectedConstitution.ToString();
        ProteinCollected.text = "Proteins: " + GameManager.collectedAgility.ToString();
    }
    
}
