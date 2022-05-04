using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlocker : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> levelsButtons;
    [SerializeField]
    private DBData _data;
    [SerializeField]
    private Firebase _db;
    bool lastLevelFinished = false;

    private void Awake()
    {
        _db = GameObject.FindGameObjectWithTag("DB").GetComponent<Firebase>();
        _db.GetUserName();
        _data = GetComponent<DBData>();
    }

    private void Start()
    {
        UnlockLevels();
    }
    public void UnlockLevels()
    {
        int i = 0;
        foreach (var level in levelsButtons)
        {
            lastLevelFinished = _data.IsLevelOpen(i);

            if (!lastLevelFinished)
            {
                level.GetComponent<Button>().interactable = false;
            }
            else
            {
                level.GetComponent<Button>().interactable = true;
            }
            level.SetActive(true);
            i++;
        }
    }
}

