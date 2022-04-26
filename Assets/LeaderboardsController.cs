using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeaderboardsController : MonoBehaviour
{
    private Firebase _db;
    [SerializeField] List<ScoreData> _scoresDesc;
    private void Awake()
    {
        _db = GameObject.FindGameObjectWithTag("DB").GetComponent<Firebase>();
        GetLevelLeaderboard("Level1");
    }
    //15 requests to get full leaderboard XDDD
    public void GetLevelLeaderboard(string levelName)
    {
        _db.iterations = 0;
        //downloads scores for specific level
        _db.GetToDBAllUsersPlease(levelName + "_1");
        _db.GetToDBAllUsersPlease(levelName + "_2");
        _db.GetToDBAllUsersPlease(levelName + "_3");
    }
    private void Update()
    {
        if (_db.iterations >= 3)
        {
            _scoresDesc = _db.scores.OrderByDescending(p => p.score).ToList();
        }
    }
}
