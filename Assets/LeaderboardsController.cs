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
    //15 requests to get full leaderboard XDDD
    public void GetLevelLeaderboard(string levelName)
    {
        _db.GetToDBAllUsersPlease(levelName + "_1");
    }
    //take scores for 1/3 of level
    public List<ScoresPlayers> GetLevelScores(string levelName)
    {
        List<ScoresPlayers> scores = new List<ScoresPlayers>();
        foreach (var score in _db.scores)
        {
            scores.Add(new ScoresPlayers 
            { 
                playerName = score.playerName,
                wallet = score.wallet,
                score = score.levels.Where(x => x.levelName == levelName).Select(p => p.score).SingleOrDefault()
            });
        }
        return scores.OrderByDescending(p => p.score).ToList();
    }

    //summary of 3 parts of level

    //summary of all levels
}
