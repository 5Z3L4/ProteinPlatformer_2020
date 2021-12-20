using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firebase : MonoBehaviour
{
    private string databaseURL = "https://metagymtest-default-rtdb.firebaseio.com/users/";
    public PlayerData dataToReturn = new PlayerData();
    private string authKey = "AIzaSyAquMeylCI4AEzjNkwVL9xJQh58EHslg8Q";
    public SaveManager SM;

    private void Start()
    {
        SM = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>();
        SignUpUser("newapitevsxccxxdvsdvst@test.com", "Apixxx_test", "twojaaastara123");
        //SignInUser("finaltest@test.com", "twojastara123");
        //PostToDB(new PlayerData
        //{
        //    playerName = "test",
        //    wallet = "testterrawallet",

        //});
        ////GetToDB("Zbigniew Handzel");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Klikn¹³eœ save");
            PostToDB();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            print(SM.level1.score);
            RetrieveFromDatabase();
            print(SM.idToken);
        }
    }
    private void RetrieveFromDatabase()
    {
        print("Current local id: " + SM.localId);
        RestClient.Get<PlayerData>(databaseURL + "/" + SM.localId + ".json?auth=" + SM.idToken).Then(response =>
        {
            SM.idToken = response.localId;
            SM.level1 = response.levels[0];
            SM.level2 = response.levels[1];
            SM.level3 = response.levels[2];
            SM.level4 = response.levels[3];
            SM.level5 = response.levels[4];
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

    public void GetToDB(string GamingName)
    {
        RestClient.Get<PlayerData>(databaseURL + GamingName + ".json").Then(callback =>
        {
            dataToReturn = callback;
        });
    }

    
    public void SignUpUser(string email, string username, string password)
    {
        string userData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>("https://identitytoolkit.googleapis.com/v1/accounts:signUp?key=" + authKey, userData).Then(
            response =>
            {
                SM.idToken = response.idToken;
                SM.localId = response.localId;
                SM.playerName = username;
                PostToDB(true);
            }).Catch(error =>
            {
                Debug.Log(error);
            });
    }

    public void SignInUser(string email, string password)
    {
        print(SM.level1.score);
        string userData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>("https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=" + authKey, userData).Then(
            response =>
            {
                SM.idToken = response.idToken;
                print(SM.idToken);
                SM.localId = response.localId;
                GetUserName();
            }).Catch(error =>
            {
                Debug.Log(error);
            });
    }

    private void GetUserName()
    {
        RestClient.Get<PlayerData>(databaseURL + "/" + SM.localId + ".json?auth=" + SM.idToken).Then(callback =>
        {
            SM.playerName = callback.playerName;
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
            levels = new LevelData[5] 
            { 
                new LevelData { levelName = "Level_1", score = SM.level1.score, deathCounter = SM.level1.deathCounter, isCompleted = SM.level1.isCompleted }, 
                new LevelData { levelName = "Level_2", score = SM.level2.score }, 
                new LevelData { levelName = "Level_3", score = SM.level3.score }, 
                new LevelData { levelName = "Level_4", score = SM.level4.score }, 
                new LevelData { levelName = "Level_5", score = SM.level5.score }, 
            }
        };

        return pd;
    }
}
