using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeaderboardsController : MonoBehaviour
{
    private Firebase _db;
    [SerializeField] List<ScoreData> _scoresDesc;
    [SerializeField] ScoreData _playerScore;
    private void Awake()
    {
        _db = GameObject.FindGameObjectWithTag("DB").GetComponent<Firebase>();
        GetLevelLeaderboard("Level1");
    }
    public void GetLevelLeaderboard(string levelName)
    {
        _db.GetToDBAllUsersPlease(levelName + "_1");
    }

    //take scores for 1/3 of level Level1_1 etc
    public List<ScoresPlayers> GetLevelPartScores(string levelName)
    {
        List<ScoresPlayers> scores = new List<ScoresPlayers>();
        List<ScoreData> tempScoreData = _db.scores;
        foreach (var score in tempScoreData)
        {
            scores.Add(new ScoresPlayers 
            { 
                playerName = score.playerName,
                wallet = score.wallet,
                score = score.levels.Where(x => x.levelName == levelName).FirstOrDefault().score
            });
        }
        return scores.OrderByDescending(p => p.score).ToList();
    }

    //summary of 3 parts of level Level1 etc
    public List<ScoresPlayers> GetWholeLevelScores(string levelName)
    {
        List<ScoresPlayers> scores = new List<ScoresPlayers>();
        foreach (var score in _db.scores)
        {
            int tempScore = 0;
            foreach (var level in score.levels)
            {
                if (level.levelName.Contains(levelName))
                {
                    tempScore += level.score;
                }
            };

            scores.Add(new ScoresPlayers
            {
                playerName = score.playerName,
                wallet = score.wallet,
                score = tempScore
            });
        }
        return scores.OrderByDescending(p => p.score).ToList();
    }

    //summary of all levels
    public List<ScoresPlayers> GetWholeGameScores()
    {
        List<ScoresPlayers> scores = new List<ScoresPlayers>();
        foreach (var score in _db.scores)
        {
            int tempScore = 0;
            foreach (var level in score.levels)
            {
                tempScore += level.score;
            };

            scores.Add(new ScoresPlayers
            {
                playerName = score.playerName,
                wallet = score.wallet,
                score = tempScore
            });
        }
        return scores.OrderByDescending(p => p.score).ToList();
    }
}
