using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuder : MonoBehaviour
{
    public bool facingRight = true;
    public float baseCastDist;
    public Transform castPos;
    [Tooltip("true -> w prawo false -> w lewo")]
    public bool leftOrRight;
    public float moveSpeed;

    private bool isFlipping = false;
    private PlayerMovement player;
    private Rigidbody2D rb;
    private Vector3 baseScale;
    public bool canAttack;
    public bool canMove;
    private float timeBtwAttack;
    private float startTimeBtwAttack;
    private Animator myAnim;
    private void Awake()
    {
        myAnim = GetComponentInChildren<Animator>();
        player = FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        baseScale = transform.localScale;
        timeBtwAttack = startTimeBtwAttack;
        canAttack = false;
        canMove = true;
        myAnim.SetFloat("MoveSpeed", moveSpeed);
        if (leftOrRight)
        {
            facingRight = true;
        }
        else
        {
            Flip();
            facingRight = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        float vX = moveSpeed;
        if (!facingRight)
        {
            vX = -moveSpeed;
        }
        if (canMove)
        {
            rb.velocity = new Vector2(vX, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (IsHittingWall() || IsNearEdge())
        {
            if (!isFlipping)
            {
                StartCoroutine(WaitBeforeFlip());
            }
        }
    }
    IEnumerator WaitBeforeFlip()
    {
        isFlipping = true;
        myAnim.SetFloat("MoveSpeed", 0);
        canMove = false;
        yield return new WaitForSeconds(1);
        Flip();
        canMove = true;
        isFlipping = false;
        myAnim.SetFloat("MoveSpeed", moveSpeed);
    }
    bool IsHittingWall()
    {
        bool val = false;
        float castDist = baseCastDist;
        if (!facingRight)
        {
            castDist = -baseCastDist;
        }
        Vector3 targetPos = castPos.position;
        targetPos.x += castDist;
        Debug.DrawLine(castPos.position, targetPos, Color.blue);
        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = true;
        }
        else
        {
            val = false;
        }
        return val;
    }
    public void Flip()
    {

        Vector3 newScale = baseScale;
        newScale.x = transform.localScale.x * -1;
        transform.localScale = newScale;
        facingRight = !facingRight;

    }
    bool IsNearEdge()
    {
        bool val = true;
        float castDist = baseCastDist;
        Vector3 targetPos = castPos.position;
        targetPos.y -= castDist;
        Debug.DrawLine(castPos.position, targetPos, Color.red);
        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = false;
        }
        else
        {
            val = true;
        }
        return val;
    }
}
