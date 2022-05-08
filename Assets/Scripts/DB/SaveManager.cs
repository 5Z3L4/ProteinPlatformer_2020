
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public ScoreData levelsScore = new ScoreData();
    private Firebase _db;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        _db = GameObject.FindGameObjectWithTag("DB").GetComponent<Firebase>();
    }
    public void UpdateDataForCurrentLevel(int levelNumber, int score, float time, int collectedChests, int collectedVMs, int collectedMeat, bool hiddenPlace, string nick)
    {
        if (DS.DecryptScore(levels[levelNumber].score) < score)
        {
            levels[levelNumber].score = DS.EncryptScore(score);
            //if it's player highscore we don't have to compare it with database
            levelsScore = _db.scores.Where(p => p.playerName == nick).SingleOrDefault();
            foreach (var item in levelsScore.levels)
            {
                if (item.levelName == levels[levelNumber].levelName)
                {
                    //set score
                    item.seed = DS.EncryptScore(score);
                }
            }
            _db.PostToDBScore(levelsScore, levels[levelNumber].levelName);
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
