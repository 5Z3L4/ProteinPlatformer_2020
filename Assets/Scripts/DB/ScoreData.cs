using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreData
{
    public string playerName;
    public string wallet;
    public List<Level> levels;
}
[System.Serializable]
public class Level
{
    //public Level()
    //{
    //    levelName = "";
    //    score = 0;
    //}
    public string levelName;
    public int score;
}

public class ScoresPlayers
{
    public string playerName;
    public string wallet;
    public int score;
}
