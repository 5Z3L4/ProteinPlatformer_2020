using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

    public string idToken;
    public string localId;
    public string playerName;
    public string terraWallet;
    public int deathCounter;
    public int currentLevelId = 2;

    public List<LevelData> levels = new List<LevelData>();
    private Firebase _db;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        _db = GameObject.FindGameObjectWithTag("DB").GetComponent<Firebase>();
    }
    private void Start()
    {
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    _db.PostToDBScore(new ScoreData { score = 5000, playerName = "testscore"}, "Level1_2");
        //}
    }
    public void UpdateDataForCurrentLevel(int levelNumber, int score, float time, int collectedChests, int collectedVMs, int collectedMeat, bool hiddenPlace )
    {
        score = 100000;
        if (levels[levelNumber].score < score)
        {
            levels[levelNumber].score = score;
            Debug.Log("score: " + levels[levelNumber].score + " player name: " + playerName + " Levels: " + levels[levelNumber].levelName);
            _db.PostToDBScore(new ScoreData { score = levels[levelNumber].score, playerName = playerName }, levels[levelNumber].levelName);
        }
        if (levels[levelNumber].bestTime > time || levels[levelNumber].bestTime == 0)
        {
            levels[levelNumber].bestTime = time;
        }
        if (levels[levelNumber].collectedChests < collectedChests)
        {
            levels[levelNumber].collectedChests = collectedChests;
        }
        if (levels[levelNumber].collectedVM < collectedVMs)
        {
            levels[levelNumber].collectedVM = collectedVMs;
        }
        if (levels[levelNumber].collectedMeat < collectedMeat)
        {
            levels[levelNumber].collectedMeat = collectedMeat;
        }
        if (!levels[levelNumber].foundHiddenPlace)
        {
            levels[levelNumber].foundHiddenPlace = hiddenPlace;
        }
        levels[levelNumber].isCompleted = true;
        _db.PostToDB(true);
    }

    public void AddTriesForCurrentLevel(int currentLevelNumber)
    {
        levels[currentLevelNumber].tries++;
    }
    //public LevelData level1 = new LevelData();
    //public LevelData level2 = new LevelData();
    //public LevelData level3 = new LevelData();
    //public LevelData level4 = new LevelData();
    //public LevelData level5 = new LevelData();
}
