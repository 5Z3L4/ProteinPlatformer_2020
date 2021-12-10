using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerStats statistics = new PlayerStats();
    //particle system
    public ParticleSystem slide;
    public ParticleSystem smash;
    public ParticleSystem charge;
    public ParticleSystem falling;
    public ParticleSystem fakeFloorBlowUp;
    public ParticleSystem fakeWallBlowUp;
    //move variables
    private float horizontalAxis;
    public float moveSpeed;
    public bool facingRight = true;

    //slide bariables
    public float slideDirection = 1;

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
    private bool isSliding;
    public Rigidbody2D playerRB;
    public float slideSpeed = 500;

    public BoxCollider2D mainCollider;
    public CircleCollider2D slideCollider;

    public bool isCharging = false;
    public bool isSmashing = false;
    private bool shouldSmashParticle;

    private void Awake()
    {

        statistics.playerPosition = this.gameObject.transform;
    }
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Sprawdzamy inputy, robimy to w Update, bo FixedUpdate jest jedynie do fizyki
        CheckAxis();
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Slide();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Charge();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && playerRB.velocity.y !=0)
        {
            Smash();  
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            print(GameManager.maxAgility);
            print(GameManager.maxConstitution);
            print(GameManager.maxStrenght);
        }

        //Sprawdzamy czy gracz dotyka pod³ogi, robimy to z zapasem ¿eby skok by³ p³ynniejszy
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if (shouldSmashParticle)
        {
            if (isGrounded==true && playerRB.velocity.y == 0 && isSmashing)
            {
                PlayParticleSystem(smash);
                shouldSmashParticle = false;
                isSmashing = false;
            }
            
        }
        statistics.playerPosition = this.gameObject.transform;
        Jump();
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
            playerRB.velocity = Vector2.up * 0;
            playerRB.velocity = Vector2.up * statistics.jumpForce;
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
    }

    public void Move(float horizontal, Rigidbody2D rb, float speed)
    {

        //poruszanie
        rb.velocity = new Vector2(horizontal * statistics.moveSpeed * Time.fixedDeltaTime, rb.velocity.y);

        //je¿eli siê gibiemy
        if (isSliding)
        {
            rb.velocity = new Vector2(slideDirection * statistics.slideSpeed * Time.fixedDeltaTime, rb.velocity.y);
            transform.eulerAngles = Vector3.forward * 70 * slideDirection;
        }

        if (isCharging)
        {
            rb.velocity = new Vector2(slideDirection * statistics.chargeSpeed * Time.fixedDeltaTime, rb.velocity.y);
        }
        
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            Flip();
        }
    }


    public void Slide()
    {
        PlayParticleSystem(slide);
        playerRB.velocity += Vector2.up * Physics2D.gravity.y * (80) * Time.deltaTime;
        mainCollider.enabled = false;
        slideCollider.enabled = true;
        isSliding = true;
        StartCoroutine("stopSlide");
    }
    IEnumerator stopSlide()
    {
        yield return new WaitForSeconds(0.8f);
        transform.eulerAngles = Vector3.zero;
        mainCollider.enabled = true;
        slideCollider.enabled = false;
        isSliding = false;
    }

    public void Charge()
    {
        PlayParticleSystem(charge);
        isCharging = true;
        StartCoroutine("stopCharge");
    }
  
    IEnumerator stopCharge()
    {
        yield return new WaitForSeconds(0.4f);
        isCharging = false;
    }

    public void Smash()
    {
        //TO DO: (smash w momencie uderzenia w ziemie)

        PlayParticleSystem(falling);
        //spadaj w dó³ a¿ nie trafisz na ziemie
        
        playerRB.constraints = RigidbodyConstraints2D.FreezeAll;
        isSmashing = true;
        StartCoroutine("stopSmash");
    }

    IEnumerator stopSmash()
    {
        yield return new WaitForSeconds(0.1f);
        playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        playerRB.velocity += Vector2.up * Physics2D.gravity.y * (250) * Time.deltaTime;
        shouldSmashParticle = true;
    }

    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
        slideDirection *= -1;
    }

    public void SpeedBoost()
    {
        statistics.moveSpeed *= 2.5f;
        StartCoroutine("speedBoostTimer");
    }
    public void ZaWarudo()
    {
        
    }
    public void JumpBoost()
    {
        statistics.jumpForce *= 1.5f;
        StartCoroutine("jumpBoostTimer");
    }

    IEnumerator speedBoostTimer()
    {
        yield return new WaitForSeconds(5);
        statistics.moveSpeed /= 2.5f;
    }
    IEnumerator jumpBoostTimer()
    {
        yield return new WaitForSeconds(5);
        statistics.jumpForce /= 1.5f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Destroyable") && isCharging)
        {
            //Play sound
            PlayParticleSystem(fakeWallBlowUp);
            ScreenShake.Instance.Shakecamera(5f, .1f);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Smashable" && isSmashing)
        {
            //Play sound
            PlayParticleSystem(fakeFloorBlowUp);
            ScreenShake.Instance.Shakecamera(5f, .1f);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "SpeedBoost")
        {
            SpeedBoost();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "JumpBoost")
        {
            JumpBoost();
            Destroy(collision.gameObject);
        }

    }
    private void PlayParticleSystem(ParticleSystem vfx)
    {
        vfx.Play();
    }

    
}
