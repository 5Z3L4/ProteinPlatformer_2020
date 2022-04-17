using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SECShieldBack : MonoBehaviour
{
    [SerializeField]
    private Transform _arnie;
    [SerializeField]
    private Animator _secAnim;
    [SerializeField]
    private float _speed;
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _arnie.position, _speed * Time.deltaTime);
    }

    private void OnEnable()
    {
        gameObject.transform.SetParent(null);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpeedBoost"))
        {
            _secAnim.SetBool("JumpBack", true);
            Destroy(gameObject);
        }
    }
}
