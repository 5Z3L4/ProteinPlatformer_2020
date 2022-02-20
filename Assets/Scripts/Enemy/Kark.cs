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
    public bool facingRight;
    Rigidbody2D rb;
    [SerializeField] float moveSpeed;
    Vector3 baseScale;
    bool canMove;
    bool canAttack;
    bool isFlipping = false;
    [Tooltip("true -> w prawo false -> w lewo")]
    public bool leftOrRight = true;
    public bool shouldAttack = true;
    public int scoreValue = 10;
    [SerializeField] private GameObject scoreText;

    public Animator myAnim;
    private bool isDying;

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
    private void Update()
    {
        timeBtwAttack -= Time.deltaTime;
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
        myAnim.SetFloat("Speed", 0);
        canMove = false;
        yield return new WaitForSeconds(1);
        Flip();
        canMove = true;
        isFlipping = false;
        myAnim.SetFloat("Speed", moveSpeed);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Enemy"))
        {
            StartCoroutine(WaitBeforeFlip());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right);
            if ((facingRight == player.facingRight && player.isSliding) || player.isCharging)
            {
                StartCoroutine(Die());
            }
            else if (facingRight != player.facingRight || facingRight == player.facingRight)
            {
                if (timeBtwAttack > 0) return;
                if ((CalculatePlayerPos() < 0 && facingRight) || CalculatePlayerPos() > 0 && !facingRight && shouldAttack)
                {
                    Flip();
                }
                canMove = false;
                canAttack = true;
                if (canAttack && shouldAttack)
                {
                    if (!isDying)
                    {
                        StartCoroutine(Punch());
                    }
                }
            }
        }
    }

    private float CalculatePlayerPos()
    {
        return player.transform.position.x - transform.position.x;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canAttack = false;

        }
    }

    IEnumerator Die()
    {
        if (!isDying) {
            SFXManager.PlaySound(SFXManager.Sound.Hit, transform.position);
        };
        isDying = true;
        shouldAttack = false;
        canMove = false;
        myAnim.Play("GetHit");
        yield return new WaitForSeconds(0.1f);
        Instantiate(scoreText, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Destroy(gameObject);
        GameManager.Score += scoreValue;
    }
    IEnumerator Punch()
    {
        timeBtwAttack = startTimeBtwAttack;
        //TO DO: sprawdzanie czy gracz nadal jest w zasiêgu w trakcie ataku
        myAnim.SetBool("Punch", true);
        yield return new WaitForSeconds(0.1f);
        myAnim.SetBool("Punch", false);
        canMove = true;
        player.TakeCertainAmountOfHp();
        //player.KnockBack(!facingRight);
        yield return new WaitForSeconds(0.2f);
        Flip();
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
