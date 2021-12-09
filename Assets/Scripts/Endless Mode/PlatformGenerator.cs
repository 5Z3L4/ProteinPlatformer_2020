using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public Transform generationPoint;
    public float distanceBetween;

    public float distanceBetweenMin;
    public float distanceBetweenMax;

    //public List<GameObject> platforms;
    private int platformSelector;
    [SerializeField]
    private List<float> platformWidths;

    private float minHeight;
    public Transform maxHeightPoint;
    private float maxHeight;
    public float maxHeightChange;
    public float heightChange;

    public CollectiblesGenerator theCollectiblesGenerator;

    public List<ObjectPooler> theObjectPools;

    public float randomCollectibleThreshold;

    public float randomSpikeThreshhold;
    public ObjectPooler spikePool;
    void Start()
    {
        platformWidths = new List<float>();

        foreach (var platform in theObjectPools)
        {
            platformWidths.Add(platform.pooledObject.GetComponent<BoxCollider2D>().size.x);
        }
        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;

        theCollectiblesGenerator = FindObjectOfType<CollectiblesGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < generationPoint.position.x)
        {
            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);
            
            platformSelector = Random.Range(0,theObjectPools.Count);
            heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);
            if (heightChange > maxHeight)
            {
                heightChange = maxHeight;
            }
            else if (heightChange < minHeight)
            {
                heightChange = minHeight;
            }
            transform.position = new Vector2(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, heightChange);

            
            //Instantiate(platforms[platformSelector], transform.position, transform.rotation);
            GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();
            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            if (Random.Range(0f, 100f) < randomCollectibleThreshold)
            {
                theCollectiblesGenerator.SpawnCollectibles(new Vector2(transform.position.x, transform.position.y + 1.5f));
            }

            if (Random.Range(0f, 100f) < randomSpikeThreshhold)
            {
                GameObject newSpike = spikePool.GetPooledObject();

                float spikeXPos = Random.Range(-platformWidths[platformSelector] / 2, platformWidths[platformSelector] / 2);
                Vector3 spikesPosition = new Vector3(spikeXPos, 0.5f);
                newSpike.transform.position = transform.position + spikesPosition;
                newSpike.transform.rotation = transform.rotation;
                newSpike.SetActive(true);
            }
            transform.position = new Vector2(transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y);
        }
    }

}
