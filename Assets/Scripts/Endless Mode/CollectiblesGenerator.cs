using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesGenerator : MonoBehaviour
{
    public List<ObjectPooler> collectiblesPool;
    private int collectibleSelector;
    public float distanceBetweenCollectibles;

    public void SpawnCollectibles(Vector2 startPosition)
    {
        Vector2 startPosHolder = startPosition;
        //middle
        collectibleSelector = Random.Range(0, collectiblesPool.Count);
        GameObject collectible1 = collectiblesPool[collectibleSelector].GetPooledObject();
        collectible1.SetActive(true);
        collectible1.transform.position = startPosHolder;

        //first
        collectibleSelector = Random.Range(0, collectiblesPool.Count);
        GameObject collectible2 = collectiblesPool[collectibleSelector].GetPooledObject();
        collectible2.SetActive(true);
        collectible2.transform.position = new Vector2(startPosition.x - distanceBetweenCollectibles, startPosHolder.y);

        //third
        collectibleSelector = Random.Range(0, collectiblesPool.Count);
        GameObject collectible3 = collectiblesPool[collectibleSelector].GetPooledObject();
        collectible3.SetActive(true);
        collectible3.transform.position = new Vector2(startPosition.x + distanceBetweenCollectibles, startPosHolder.y);


    }
}
