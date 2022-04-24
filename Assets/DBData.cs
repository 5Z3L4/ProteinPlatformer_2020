using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DBData : MonoBehaviour
{
    public SaveManager _saveMan;

    private void Awake()
    {
        _saveMan = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>();
    }

    public bool IsLevelOpen(int levelNumber)
    {
        //LevelData currentLevel = _saveMan.levels.Where(p => p.levelName == "levelname").FirstOrDefault();
        LevelData currentLevel = _saveMan.levels[levelNumber];
        
        if (currentLevel.isCompleted)
        {
            return true;
        }
        
        return false;
    }
}
