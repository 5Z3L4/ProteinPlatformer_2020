using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject ticket;
    private void Start()
    {
        Instantiate(ticket, transform.position, Quaternion.identity);
    }
}
