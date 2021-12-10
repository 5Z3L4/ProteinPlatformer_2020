using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndlessModeUI : MonoBehaviour
{
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
        Score.text = "Your score: " + score.ToString();
        DumbbleCollected.text = "Dumbbles: " + dumbbleAmount.ToString();
        MeatCollected.text = "Meat: " + meatAmount.ToString();
        ProteinCollected.text = "Proteins: " + proteinAmount.ToString();
        //TO DO: pobierac wartosci z GameManagera
    }
    public void RestartButton()
    {
        Time.timeScale = 1;
        DeathScreen.SetActive(false);
        theGameManager.RestartGame();
    }
    public void ShowDeathScreen()
    {
        Time.timeScale = 0;
        DeathScreen.SetActive(true);
    }
}
