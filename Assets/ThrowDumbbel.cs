using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowDumbbel : MonoBehaviour
{
    public GameObject dumbbel;
    public float startTimeBtwAttack;
    public bool canIShoot = false;
    float timeBtwAttack =0;

    void Update()
    {
        if (!canIShoot) return;

        if (timeBtwAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Instantiate(dumbbel, transform.position, transform.rotation);
                timeBtwAttack = startTimeBtwAttack;
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
        
    }
}
