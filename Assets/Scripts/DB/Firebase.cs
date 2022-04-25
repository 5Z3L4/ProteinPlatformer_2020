using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firebase : MonoBehaviour
{
    private string databaseURL = "https://metagymtest-default-rtdb.firebaseio.com/users/";
    public PlayerData dataToReturn = new PlayerData();
    public Players dataToReturnList = new Players();
    private string authKey = "AIzaSyAquMeylCI4AEzjNkwVL9xJQh58EHslg8Q";
    public SaveManager SM;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        SM = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>();
        //SignInUser("test123", "test123");
        GetToDBAllUsersPlease("x");
        //SignUpUser("testlistyaa@test.com", "lista", "twojaaastara123");
        
    }

    //delete this
    private void Update()
    {
        if (!string.IsNullOrEmpty(GameManager.response))
        {
            Dictionary<string, PlayerData> entryDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, PlayerData>>(GameManager.response);
        }

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
        RestClient.Put("https://metagymtest-default-rtdb.firebaseio.com/Scores/" + levelName + "/" + SM.localId + ".json?auth=" +SM.idToken, score).Catch(error => { Debug.Log(error); });
        Debug.Log("Rzuci³o puta");
    }

    public void GetToDB(string GamingName)
    {
        RestClient.Get<PlayerData>(databaseURL + GamingName + ".json").Then(callback =>
        {
            dataToReturn = callback;
        });
    }
    
    public void GetToDBAllUsersPlease(string GamingName)
    {
        RestClient.Get<Players>(
        "https://metagymtest-default-rtdb.firebaseio.com/users.json", "x").Then(response =>
        {
            dataToReturnList = response;
 
        });
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
