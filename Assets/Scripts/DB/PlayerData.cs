using System.Collections.Generic;

[System.Serializable]

public class PlayerData
{
    public string playerName;
    public string wallet;
    public string localId;
    public List<LevelData> levels = new List<LevelData>();
}
