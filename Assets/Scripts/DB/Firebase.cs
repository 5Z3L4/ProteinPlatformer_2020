using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Firebase : MonoBehaviour
{
    private string databaseURL = "https://metagymtest-default-rtdb.firebaseio.com/users/";
    public PlayerData dataToReturn = new PlayerData();
    public Players dataToReturnList = new Players();
    private string authKey = "AIzaSyAquMeylCI4AEzjNkwVL9xJQh58EHslg8Q";
    public SaveManager SM;
    public List<ScoreData> scores = new List<ScoreData>();
    public int iterations = 0;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        SM = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>();
    }

    //delete this
    private void Update()
    {
        //deserialize score for leaderboard
        //if (!string.IsNullOrEmpty(GameManager.response))
        //{
        //    Dictionary<string, ScoreData> entryDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, ScoreData>>(GameManager.response);
        //}

        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    Debug.Log("Klikn¹³eœ save");
        //    PostToDB();
        //}
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    RetrieveFromDatabase();
        //    print(SM.idToken);
        //}
    }
    private void RetrieveFromDatabase()
    {
        print("Current local id: " + SM.localId);
        RestClient.Get<PlayerData>(databaseURL + "/" + SM.localId + ".json?auth=" + SM.idToken).Then(response =>
        {
            SM.levels = response.levels;
            SM.playerName = response.playerName;
            SM.terraWallet = response.wallet;
            SM.localId = response.localId;
            //UpdateScore();
        });
    }
    private ScoreData InitializeList(string nick, string wallet)
    {
        return new ScoreData { playerName = nick, wallet = wallet, levels = new List<Level>
        {
            new Level
            {
                levelName="Level1_1",
                score=0
            },
            new Level
            {
                levelName="Level1_2",
                score=0
            },
            new Level
            {
                levelName="Level1_3",
                score=0
            },
            new Level
            {
                levelName="Level2_1",
                score=0
            },
            new Level
            {
                levelName="Level2_2",
                score=0        
            },                 
            new Level          
            {                  
                levelName="Level2_3",
                score=0
            },
            new Level
            {
                levelName="Level3_1",
                score=0
            },
            new Level
            {
                levelName="Level3_2",
                score=0
            },
            new Level
            {
                levelName="Level3_3",
                score=0
            },
            new Level
            {
                levelName="Level4_1",
                score=0
            },
            new Level
            {
                levelName="Level4_2",
                score=0        
            },                 
            new Level          
            {                  
                levelName="Level4_3",
                score=0
            },
            new Level
            {
                levelName="Level5_1",
                score=0
            },
            new Level
            {
                levelName="Level5_2",
                score=0        
            },                 
            new Level          
            {                  
                levelName="Level5_3",
                score=0
            }
        } };
    }
    public void PostToDB(bool emptyScore = false)
    {
        PlayerData playerData = DataCollector();
        if (SM.localId == null)
        {
            print("local id jest nullem");
            return;
        }
        RestClient.Put("https://metagymtest-default-rtdb.firebaseio.com/users/" + SM.localId + ".json?auth=" +SM.idToken, playerData).Catch(error => { Debug.Log(error); });
        Debug.Log("Rzuci³o puta");
    }
    public void PostToDBScore(ScoreData score, string levelName)
    {
        if (SM.localId == null)
        {
            print("local id jest nullem");
            return;
        }
        RestClient.Put("https://metagymtest-default-rtdb.firebaseio.com/Scores/" + SM.localId + ".json?auth=" +SM.idToken, score).Catch(error => { Debug.Log(error); });
        Debug.Log("Rzuci³o puta z wynikiem");
    }

    public void GetToDB(string GamingName)
    {
        RestClient.Get<PlayerData>(databaseURL + GamingName + ".json").Then(callback =>
        {
            dataToReturn = callback;
        });
    }
    
    public void GetToDBAllUsersPlease(string levelName)
    {
        //reseting scores
        scores.Clear();

        RestClient.Get<Players>(
        "https://metagymtest-default-rtdb.firebaseio.com/Scores.json", "x").Then(response =>
        {
            Dictionary<string, ScoreData> entryDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, ScoreData>>(GameManager.response);
            scores.AddRange(entryDict.Select(p => p.Value).ToList());
            iterations++;
        }).Catch(error => { Debug.Log(error); });
    }

    
    public void SignUpUser(string email, string username, string password)
    {
        string userData = "{\"email\":\"" + email + "@metagym.io" + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>("https://identitytoolkit.googleapis.com/v1/accounts:signUp?key=" + authKey, userData).Then(
            response =>
            {
                SM.idToken = response.idToken;
                SM.localId = response.localId;
                SM.playerName = username;
                PostToDB(true);
                SignInUser(email, password);
                PostToDBScore(InitializeList(email, "placeholder"), "x");
            }).Catch(error =>
            {
                Debug.Log(error.Message);
            });
    }

    public void SignInUser(string email, string password)
    {
        string userData = "{\"email\":\"" + email + "@metagym.io" + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>("https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=" + authKey, userData).Then(
            response =>
            {
                SM.idToken = response.idToken;
                print(SM.idToken);
                SM.localId = response.localId;
                GetUserName();
            }).Catch(error =>
            {
                Debug.Log(error.Message);
            });
    }

    public void GetUserName()
    {
        RestClient.Get<PlayerData>(databaseURL + "/" + SM.localId + ".json?auth=" + SM.idToken).Then(callback =>
        {
            SM.playerName = callback.playerName;
            SM.levels = callback.levels;
            SM.terraWallet = callback.wallet;
        });
    }
    public void GetLeaderBoard()
    {
        RestClient.Get(databaseURL + ".json?auth=" + SM.idToken).Then(callback =>
        {
            string x = callback.Headers.ToString();
        }).Catch(error =>
        {
            Debug.Log(error.Message);
        });
    }
    
    //save current data
    private void SetTerraWallet()
    {
        SM.terraWallet = "test";
    }
    //get leaderboard
    //refresh token

    public PlayerData DataCollector()
    {
        PlayerData pd = new PlayerData()
        {
            playerName = SM.playerName,
            localId = SM.localId,
            wallet = SM.terraWallet,
            levels = SM.levels
        };

        return pd;
    }
}
