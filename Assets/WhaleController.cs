using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleController : MonoBehaviour
{
    public GameObject Bag;
    public void Spawnbag()
    {
        Instantiate(Bag);
    }

}
