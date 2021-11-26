using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //move variables
    private float horizontalAxis;
    public float moveSpeed;
    public bool facingRight = true;

    //jump variables
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float jumpForce = 5;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    private bool isGrounded;
    private bool isJumping;
    private bool isJumpingLow;

    public Rigidbody2D playerRB;

    private void Awake()
    {
        PlayerStats.playerPosition = this.gameObject.transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Sprawdzamy inputy, robimy to w Update, bo FixedUpdate jest jedynie do fizyki
        CheckAxis();

        //Sprawdzamy czy gracz dotyka pod³ogi, robimy to z zapasem ¿eby skok by³ p³ynniejszy
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        PlayerStats.playerPosition = this.gameObject.transform;
        //if (!playerStats.isDead)
        //{
        Jump();
        //}
    }

    private void FixedUpdate()
    {
        //if (!playerStats.isDead)
        //{
        //Poruszanie siê - fizyka zawsze w FixedUpdate
        Move(horizontalAxis, playerRB, moveSpeed);

        //}
        //else
        //{
        //    playerRB.velocity = Vector2.zero;
        //}
    }

    private void Jump()
    {
        //je¿eli gracz wcisn¹³ spacjê i wykryliœmy ¿e dotkn¹³ ziemi
        if (isJumping && isGrounded)
        {
            playerRB.velocity = Vector2.up * PlayerStats.jumpForce;
        }
        //je¿eli gracz spada
        if (playerRB.velocity.y < 0)
        {
            //szybciej spadamy
            playerRB.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        //dostosowujemy wysokoœæ skoku do czasu trzymania spacji
        else if (playerRB.velocity.y > 0 && !isJumpingLow)
        {
            playerRB.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void CheckAxis()
    {
        horizontalAxis = Input.GetAxisRaw("Horizontal");
        isJumping = Input.GetButtonDown("Jump");
        isJumpingLow = Input.GetButton("Jump");

        //if (horizontalAxis != 0)
        //{
        //    anim.SetBool("isRunning", true);
        //}
        //else
        //{
        //    anim.SetBool("isRunning", false);
        //}
    }

    public void Move(float horizontal, Rigidbody2D rb, float speed)
    {
        //Sound manager bêdzie statyczny
        //if (!isJumping && !isJumpingLow && horizontalAxis != 0 && !SceneLoader.gamePaused)
        //{
        //    SoundManager.PlaySound(SoundManager.Sound.PlayerMove, groundCheck.transform.position);
        //}
        rb.velocity = new Vector2(horizontal * PlayerStats.moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
        
        //na razie nie robi nic dopóki nie dodam animatora
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            Flip();
        }
    }

    public void Flip()
    {
        //if (!SceneLoader.gamePaused)
        //{
        //    facingRight = !facingRight;
        //    Vector3 scaler = transform.localScale;
        //    scaler.x *= -1;
        //    transform.localScale = scaler;
        //}
    }

}
