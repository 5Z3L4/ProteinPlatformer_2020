using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecHantel : MonoBehaviour
{
    [SerializeField]
    private ThrowDumbbel _throw;
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
    }

    private void OnEnable()
    {
        _throw.canIShoot = true;
    }

}
