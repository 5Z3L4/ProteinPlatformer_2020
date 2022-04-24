using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public string levelName;
    public bool isCompleted;
    public int score;
    public int deathCounter;
    public int tries;
    public int collectedMeat;
    public int collectedVM;
    public int collectedChests;
    public float bestTime;
    public bool foundHiddenPlace;
}
