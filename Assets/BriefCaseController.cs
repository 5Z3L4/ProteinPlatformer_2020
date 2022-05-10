using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefCaseController : MonoBehaviour
{
    private bool _canHit = true;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("x"))
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _canHit)
        {
            collision.gameObject.GetComponent<PlayerMovement>().TakeCertainAmountOfHp();
            _canHit = false;
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
    public void ChangeBoolToTrue()
    {
        GameObject.Find("SEC").GetComponent<Animator>().SetBool("FinalAttack", true);
    }
    public void ChangeBoolToFalse()
    {
        GameObject.Find("SEC").GetComponent<Animator>().SetBool("FinalAttack", false);
    }
}
