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
    public List<ScoresPlayers> GetLevelScores(string levelName)
    {
        List<ScoresPlayers> scores = new List<ScoresPlayers>();
        //List<ScoreData> tempScores = _db.scores.Where(p =>p.)
        return scores;
    }
}
