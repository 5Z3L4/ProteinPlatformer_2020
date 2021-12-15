using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public Firebase db;

    public PlayerData dataToSave;

    public string terraWallet;

    public string nickName = "SaveManager2";

    public LevelData level1;
    public LevelData level2;
    public LevelData level3;
    public LevelData level4;
    public LevelData level5;

    private void Start()
    {
        db.GetToDB(nickName);
        //db.PostToDB(CreateDataToSave());
        StartCoroutine("WaitForResponse");
    }

    public void LoadData(PlayerData dataFromBase)
    {
        terraWallet = dataFromBase.wallet;
        level1 = dataFromBase.levels[0];
        level2 = dataFromBase.levels[1];
        level3 = dataFromBase.levels[2];
        level4 = dataFromBase.levels[3];
        level5 = dataFromBase.levels[4];
    }
    public PlayerData CreateDataToSave()
    {
        return new PlayerData
        {
            wallet = terraWallet,
            playerName = nickName,
            levels = new LevelData[] { level1, level2, level3, level4, level5 }
        };
    }

    IEnumerator WaitForResponse()
    {
        yield return new WaitForSeconds(0.5f);
        LoadData(db.dataToReturn);
    }
}
