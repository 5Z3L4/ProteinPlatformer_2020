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
        //SignUpUser("testlistyaa@test.com", "lista", "twojaaastara123");
        SignInUser("testlistyaa@test.com", "twojaaastara123");
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
            RetrieveFromDatabase();
            print(SM.idToken);
        }
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
            levels = SM.levels
        };

        return pd;
    }
}
