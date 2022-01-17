using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YachMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    public float timeBetweenMove = 5f;
    private float timeholder;
    private float secondTimeholder;
    private float rotationPoint = 180f;
    public bool goLeft = false;
    public GameObject waterParticle;
    //private ParticleSystem.EmissionModule waterEmission;

    // Update is called once per frame

    private void Start()
    {
        
        //waterEmission = waterParticle.emission;
        if (goLeft)
        {
            rotationPoint = 0;
            speed = -speed;
        }
        timeholder = 5f;
        secondTimeholder = timeBetweenMove;
    }
    void Update()
    {
        timeholder -= Time.deltaTime;
        if (timeholder <= 0)
        {
            waterParticle.SetActive(true);
            if (secondTimeholder > 0)
            {
                Quaternion target;
                if (goLeft)
                {
                     target = Quaternion.Euler(0, 180, 15f);
                }
                else
                {
                    target = Quaternion.Euler(0, 0, 15f);
                }
                transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * speed);
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
                secondTimeholder -= Time.deltaTime;
            }
            if (secondTimeholder <= 0)
            {
                transform.rotation = Quaternion.Euler(0, rotationPoint, 15f);
                if (rotationPoint == 180)
                {
                    rotationPoint = 0;
                }
                else
                {
                    rotationPoint = 180;
                }
                secondTimeholder = timeBetweenMove;
                speed = -speed;
            }
        }
        
    }
}
