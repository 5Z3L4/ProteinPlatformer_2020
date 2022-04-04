using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleSpawner : MonoBehaviour
{
    public List<GameObject> ObjectsToSpawn;
    public Transform SpawnPos;

    [ContextMenu("Spawn")]
    public void SpawnRandomObject()
    {
        Instantiate(ObjectsToSpawn[Random.Range(0, 2)], SpawnPos);
    }
}
