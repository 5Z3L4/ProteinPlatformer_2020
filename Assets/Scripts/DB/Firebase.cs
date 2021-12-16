using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firebase : MonoBehaviour
{
    public string firebaseLink;
    public PlayerData dataToReturn = new PlayerData();
    private void Start()
    {

        //PostToDB(new PlayerData
        //{
        //    playerName = "test",
        //    wallet="testterrawallet",
            
        //});
        ////GetToDB("Zbigniew Handzel");
    }

    public void PostToDB(PlayerData playerData)
    {
        RestClient.Put(firebaseLink + playerData.playerName + ".json", playerData);
    }

    public void GetToDB(string GamingName)
    {
        RestClient.Get<PlayerData>(firebaseLink + GamingName + ".json").Then(callback =>
        {
            dataToReturn.levels = callback.levels;
            dataToReturn.playerName = callback.playerName;
            dataToReturn.wallet = callback.wallet;
        });
    }

    
    public void DruknijDate(string health, string damage)
    {
        print("Zbigniew Handzel" + health + damage);
    }
}
