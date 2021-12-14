using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firebase : MonoBehaviour
{
    public string firebaseLink;

    private void Start()
    {
        //PostToDB(new PlayerData
        //{
        //    name = "Zbigniew Handzel",
        //    health = "zrób",
        //    damage = "mi louda"
        //});
        GetToDB("Zbigniew Handzel");
    }

    public void PostToDB(PlayerData playerData)
    {
        RestClient.Put(firebaseLink + playerData.name + ".json", playerData);
    }

    public void GetToDB(string GamingName)
    {
        RestClient.Get<PlayerData>(firebaseLink + GamingName + ".json").Then(callback =>
        {
            DruknijDate(callback.health, callback.damage);
        });
    }

    public void DruknijDate(string health, string damage)
    {
        print("Zbigniew Handzel" + health + damage);
    }
}
