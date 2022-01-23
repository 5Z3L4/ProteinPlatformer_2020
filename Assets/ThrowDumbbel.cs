using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowDumbbel : MonoBehaviour
{
    public GameObject dumbbel;
    public float startTimeBtwAttack;
    float timeBtwAttack;

    private void Start()
    {
        timeBtwAttack = startTimeBtwAttack;
    }
    void Update()
    {
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
