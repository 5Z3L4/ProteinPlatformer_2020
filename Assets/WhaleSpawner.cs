using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleSpawner : MonoBehaviour
{
    public List<GameObject> ObjectsToSpawn;
    public Transform SpawnPos;
    public bool StopSpawn = true;
    public int SpawnedAmount = 0;

    [ContextMenu("Spawn")]
    public void SpawnRandomObject()
    {
        if (StopSpawn) return;
        if (SpawnedAmount < 3)
        {
            Instantiate(ObjectsToSpawn[Random.Range(0, 2)], SpawnPos);
            SpawnedAmount++;
        }
        else
        {
            //maybe timer
            Instantiate(ObjectsToSpawn[2], SpawnPos);
        }
    }
}
