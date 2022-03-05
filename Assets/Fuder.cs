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
    public float projectileSpeed = 20f;
    public float attackRange = 10f;
    public float timeBtwAttack;
    public float startTimeBtwAttack;
    public GameObject projectile;
    public bool fuderAttacking = false;

    private bool isFlipping = false;
    private PlayerMovement player;
    private Rigidbody2D rb;
    private Vector3 baseScale;
    public bool canAttack;
    public bool canMove;
    private Animator myAnim;
    private bool playerOnRight;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask playerLayer;


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
        CheckPlayerPos(player.transform.position);

        if (Mathf.Abs(Vector2.Distance(player.transform.position, transform.position)) <= attackRange && ((facingRight && playerOnRight) || (!facingRight && !playerOnRight)))
        {
            RaycastHit2D hitGround = Physics2D.Linecast(transform.position, player.transform.position, groundLayer);
            if (hitGround.collider == null)
            {
                RaycastHit2D hitPlayer = Physics2D.Linecast(transform.position, player.transform.position, playerLayer);
                if (hitPlayer.collider != null)
                {
                    if (hitPlayer.collider.gameObject.CompareTag("Player"))
                    {
                        canAttack = true;
                        canMove = false;
                        myAnim.SetFloat("MoveSpeed", 0);
                        timeBtwAttack -= Time.deltaTime;
                    }
                }
            }
            Debug.DrawLine(transform.position, player.transform.position);
        }
        else
        {
            canAttack = false;
            if (!isFlipping)
            {
                canMove = true;
                myAnim.SetFloat("MoveSpeed", moveSpeed);
            }
        }
        
        if (canAttack)
        {
            StartCoroutine(ShootPoision());
        }
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

    IEnumerator ShootPoision()
    {
        if (timeBtwAttack <= 0)
        {
            timeBtwAttack = startTimeBtwAttack;
            myAnim.Play("Attack");
            projectile.SetActive(true);
            yield return new WaitUntil(() => !fuderAttacking);
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
    private void CheckPlayerPos(Vector2 playerPos)
    {
        if ((playerPos.x - transform.position.x) >= 0)
        {
            playerOnRight = true;
        }
        else if ((playerPos.x - transform.position.x) < 0)
        {
            playerOnRight = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (player.isSmashing && player.transform.position.y > transform.position.y)
            {

            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    public void AlertObservers(string message)
    {
        if (message.Equals("AttackAnimationEnded"))
        {
            fuderAttacking = false;
        }
    }
}
