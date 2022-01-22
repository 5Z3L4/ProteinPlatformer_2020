using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowDumbbel : MonoBehaviour
{
    public GameObject dumbbel;
    PlayerMovement player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(dumbbel, transform.position, transform.rotation);
        }
    }
}
