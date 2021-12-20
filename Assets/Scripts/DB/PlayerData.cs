[System.Serializable]

public class PlayerData
{
    public string playerName;
    public string wallet;
    public string localId;
    public LevelData[] levels = new LevelData[5];
}
