using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Leaderboards : MonoBehaviour
{
    public RectTransform yourScore;
    public RectTransform template;
    public RectTransform contentHolder;
    public List<GameObject> playerRecords = new List<GameObject>();
    public Sprite[] thropiees;
    [SerializeField]
    private LeaderboardsController _LC;
    private SaveManager _SM;
    private void Awake()
    {
        _LC = FindObjectOfType<LeaderboardsController>();
        _SM = FindObjectOfType<SaveManager>();
    }
    private void OnEnable()
    {
        LoadGeneralData();
    }
    public void LoadGeneralData()
    {
        ClearResults();
        List<ScoresPlayers> records = _LC.GetWholeGameScores();
        
        int pos = 1;
        if (pos <= 100)
        {
            foreach (var record in records)
            {
                GetPersonalResult(record, pos);
                
                GameObject template = Instantiate(this.template.gameObject, contentHolder);
                template.SetActive(true);
                SetPucharek(pos, template);
                //if (pos < 17)
                //{
                //    GameObject thropeeImg = template.transform.GetChild(0).transform.GetChild(0).gameObject;
                //    thropeeImg.SetActive(true);
                //    thropeeImg.GetComponent<Image>().sprite = thropiees[pos - 1];
                //    if (pos == 4 || pos == 8 || pos == 12 || pos == 16)
                //    {
                //        thropeeImg.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
                //    }
                //}
                TMP_Text position = template.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
                TMP_Text name = template.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
                TMP_Text playerScore = template.transform.GetChild(2).gameObject.GetComponent<TMP_Text>();
                position.SetText(pos + ". ");
                name.SetText(record.playerName);
                playerScore.SetText(record.score.ToString());
                
                pos++;
                playerRecords.Add(template);
            }
        }
    }
    public void LoadDataForChapter(string chapterName)
    {
        ClearResults();
        List<ScoresPlayers> records = _LC.GetLevelPartScores(chapterName);
        int pos = 1;
        if (pos <= 100)
        {
            foreach (var record in records)
            {
                GetPersonalResult(record, pos);

                GameObject template = Instantiate(this.template.gameObject, contentHolder);
                template.SetActive(true);
                SetPucharek(pos, template);
                TMP_Text position = template.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
                TMP_Text name = template.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
                TMP_Text playerScore = template.transform.GetChild(2).gameObject.GetComponent<TMP_Text>();
                position.SetText(pos + ". ");
                name.SetText(record.playerName);
                playerScore.SetText(record.score.ToString());
                pos++;
                playerRecords.Add(template);
            }
        }
    }
    public void LoadDataForLevel(string levelName)
    {
        ClearResults();
        List<ScoresPlayers> records = _LC.GetWholeLevelScores(levelName);
        int pos = 1;
        if (pos <= 100)
        {
            foreach (var record in records)
            {
                GetPersonalResult(record, pos);

                GameObject template = Instantiate(this.template.gameObject, contentHolder);
                template.SetActive(true);
                SetPucharek(pos, template);
                TMP_Text position = template.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
                TMP_Text name = template.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
                TMP_Text playerScore = template.transform.GetChild(2).gameObject.GetComponent<TMP_Text>();
                position.SetText(pos + ". ");
                name.SetText(record.playerName);
                playerScore.SetText(record.score.ToString());
                pos++;
                playerRecords.Add(template);
            }
        }
    }
    private void ClearResults()
    {
        foreach (var record in playerRecords)
        {
            Destroy(record);
        }
        playerRecords.Clear();
    }
    private void GetPersonalResult(ScoresPlayers record, int pos)
    {
        if (record.playerName == _SM.playerName)
        {
            yourScore.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().SetText(pos + ". ");
            yourScore.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().SetText(record.playerName);
            yourScore.transform.GetChild(3).gameObject.GetComponent<TMP_Text>().SetText(record.score.ToString());
        }
    }
    private void SetPucharek(int pos, GameObject template)
    {
        if (pos < 17)
        {
            GameObject thropeeImg = template.transform.GetChild(0).transform.GetChild(0).gameObject;
            thropeeImg.SetActive(true);
            thropeeImg.GetComponent<Image>().sprite = thropiees[pos - 1];
            if (pos == 4 || pos == 8 || pos == 12 || pos == 16)
            {
                thropeeImg.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
            }
        }
    }
}
