using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kark : MonoBehaviour //,Enemy
{
    PlayerMovement player;
    [SerializeField] float timeBtwAttack;
    [SerializeField] float startTimeBtwAttack;
    [SerializeField] Transform castPos;
    [SerializeField] float baseCastDist;
    bool facingRight;
    Rigidbody2D rb;
    [SerializeField] float moveSpeed;
    Vector3 baseScale;
    bool canMove;
    bool canAttack;
    bool startedAttack;
    [Tooltip("true -> w prawo false -> w lewo")]
    public bool leftOrRight = true;
    public bool shouldAttack = true;
    public int scoreValue = 10;

    public Animator myAnim;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        myAnim.SetFloat("Speed", moveSpeed);
        timeBtwAttack = startTimeBtwAttack;
        canAttack = false;
        canMove = true;
        baseScale = transform.localScale;
 
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
            Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right);
            if (facingRight == player.facingRight && player.isSliding)
            {
                Destroy(gameObject);
                GameManager.Score += scoreValue;
            }
            else if (facingRight != player.facingRight || facingRight == player.facingRight ) // TO DO: poprawiæ to tak, ¿eby kark siê odwraca³ jak gracz podejdzie do niego od ty³u
            {
                canMove = false;
                canAttack = true;
                if (canAttack && shouldAttack)
                {
                    StartCoroutine(Punch());
                }
            }
        }
        if (collision.CompareTag("Enemy"))
        {
            Flip();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canAttack = false;
            timeBtwAttack = startTimeBtwAttack;
        }
    }

    IEnumerator Punch()
    {
        //TO DO: sprawdzanie czy gracz nadal jest w zasiêgu w trakcie ataku
        myAnim.SetBool("Punch", true);
        yield return new WaitForSeconds(0.3f);
        myAnim.SetBool("Punch", false);
        yield return new WaitForSeconds(0.3f);
        Flip();
        startedAttack = true;
        canMove = true;
        player.TakeCertainAmountOfHp();
        //player.KnockBack(!facingRight);
        print("Player hp: " + player.hp);
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
    void Flip()
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
