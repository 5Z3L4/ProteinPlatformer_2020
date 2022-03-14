using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBalls : MonoBehaviour
{
    public List<Transform> PosList;
    public GameObject ObjToSpawn;

    public void SpawnObjects()
    {
        foreach (var pos in PosList)
        {
            Instantiate(ObjToSpawn, pos.position, Quaternion.identity);
        }
    }
}
