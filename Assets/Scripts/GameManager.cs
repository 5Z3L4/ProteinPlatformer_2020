using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    public static int collectedStrenght = 0;
    public static int maxStrenght = 0;

    public static int collectedAgility = 0;
    public static int maxAgility = 0;

    public static int collectedConstitution = 0;
    public static int maxConstitution = 0;

    public static int Score = 0;

    public static bool isStoryMode = true;

    public static int deaths = 0;

    public static int collectiblesOnMap;

    public static int chestsOnMap;
    public static int collectedChests;
    
    public static int vendingMachinesOnMap;
    public static int collectedVendingMachines;

    public static int specificLevelItemOnMap;
    public static int collectedSpecificItems;

    public static string response;
    public static void Reset()
    {
        specificLevelItemOnMap = 0;
        collectedSpecificItems = 0;
        deaths = 0;
        Score = 0;
        collectedConstitution = 0;
        maxConstitution = 0;
        collectedStrenght = 0;
        maxStrenght = 0;
        collectedAgility = 0;
        maxAgility = 0;
        chestsOnMap = 0;
        collectedChests = 0;
        vendingMachinesOnMap = 0;
        collectedVendingMachines = 0;
        collectiblesOnMap = 0;
    }

    public static void AddMeat(PlayerMovement player)
    {
        collectedConstitution++;
        if (collectedConstitution % 50 == 0)
        {
            player.maxHp++;
            player.hp++;
        }
    }
}
