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

    public LevelData level1 = new LevelData();
    public LevelData level2 = new LevelData();
    public LevelData level3 = new LevelData();
    public LevelData level4 = new LevelData();
    public LevelData level5 = new LevelData();
}
