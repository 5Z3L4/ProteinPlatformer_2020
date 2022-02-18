using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBankNote : MonoBehaviour
{
    public bool Shot;
    public GameObject BankNote;
    private void Update()
    {
        if (Shot)
        {
            Spawn();
            Shot = false;
        }
    }
    void Spawn()
    {
        Instantiate(BankNote, transform.position, Quaternion.identity);
        //zmieñ animacje
    }

}
