using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public void StartMovingTowards(string targetName, float speed)
    {
        Vector3 target = GameObject.Find(targetName).transform.position;
        transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
    }
    private void Update()
    {
        StartMovingTowards("Player", 2f);   
    }
}
