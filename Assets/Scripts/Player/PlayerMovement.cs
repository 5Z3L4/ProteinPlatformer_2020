using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //dialogue system
    [SerializeField] private DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;
    public IInteractable Interactable { get; set; }
    public bool canMove = true;

    public PlayerStats statistics = new PlayerStats();
    //particle system
    public ParticleSystem slide;
    private ParticleSystem.EmissionModule slideEmission;
    public ParticleSystem smash;
    public ParticleSystem charge;
    public ParticleSystem falling;
    public ParticleSystem fakeFloorBlowUp;
    public ParticleSystem fakeWallBlowUp;
    //move variables
    private float horizontalAxis;
    public float moveSpeed;
    public bool facingRight = true;

    //slide variables
    public float slideDirection = 1;

    //jump variables
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float jumpForce = 5;
    public float basejumpForce;
    public bool isOnJumpBoost = false;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    private bool isGrounded;
    private bool isGroundedWithoutOffset;
    private bool isJumping;
    private bool shouldJump;
    private bool isJumpingLow;
    public bool isSliding;
    public Rigidbody2D playerRB;
    public float slideSpeed = 500;

    public CapsuleCollider2D mainCollider;
    public CircleCollider2D slideCollider;
    public Animator playerAnim;
    public GameObject wallCheck;

    private float jumpTimeCounter;
    public float jumpTime = 1f;


    public bool isCharging = false;
    public bool isSmashing = false;
    private bool shouldSmashParticle;

    public Vector3 respawnPos;
    public Vector3 startingPos;
    public int hp = 3;

    public Transform PlayerBubbleTransform;

    public SaveManager SM;
    public HUDManager HUDManager;

    private void Awake()
    {
        playerAnim = GameObject.FindGameObjectWithTag("PlayerSprite").GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody2D>();
        HUDManager = FindObjectOfType<HUDManager>();
    }
    void Start()
    {
        slideEmission = slide.emission;
        statistics.playerPosition = this.gameObject.transform;
        respawnPos = transform.position;
        startingPos = transform.position;
        basejumpForce = jumpForce;
        //SM = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>();
        //SM.levels[SM.currentLevelId].levelName = "Level_" + SM.currentLevelId;
    }

    // Update is called once per frame
    void Update()
    {
        //Sprawdzamy inputy, robimy to w Update, bo FixedUpdate jest jedynie do fizyki
        CheckAxis();
        if (Input.GetKeyDown(KeyCode.LeftControl) && canMove)
        {    
            Slide();
        }
        if (isGroundedWithoutOffset && isSliding)
        {
            slideEmission.rateOverTime = 60f;
        }
        else
        {
            slideEmission.rateOverTime = 0;
        }
        if (Input.GetKeyDown(KeyCode.Tab) && canMove)
        {
            Charge();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && playerRB.velocity.y !=0)
        {
            Smash();  
        }

        //Sprawdzamy czy gracz dotyka pod³ogi, robimy to z zapasem ¿eby skok by³ p³ynniejszy
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        isGroundedWithoutOffset = Physics2D.OverlapCircle(groundCheck.position, 0.1f, whatIsGround);
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
        if (canMove)
        {
            Jump();
        }
        

        if (Input.GetKeyDown(KeyCode.E) && !dialogueUI.IsOpen)
        {
            Interactable?.Interact(this);
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Move(horizontalAxis, playerRB, moveSpeed);
        }
    }

    private void Jump()
    {
        if (isGroundedWithoutOffset)
        {
            playerAnim.SetBool("IsJumping", false);
            playerAnim.SetBool("IsFalling", false);
        }
        
        //je¿eli gracz wcisn¹³ spacjê i wykryliœmy ¿e dotkn¹³ ziemi
        if (isGrounded && Input.GetKeyDown("space") && !shouldJump)
        {
            mainCollider.enabled = true;
            SFXManager.PlaySound(SFXManager.Sound.Jump, transform.position);
            playerAnim.SetBool("IsSliding", false);
            shouldJump = true;
            jumpTimeCounter = jumpTime;
            //playerRB.velocity = Vector2.up * 0;
            playerRB.velocity = Vector2.up * jumpForce;
        }

        //je¿eli gracz spada
        if (playerRB.velocity.y < 0 && !isGrounded)
        {
            playerAnim.SetBool("IsFalling", true);
            playerAnim.SetBool("IsJumping", false);
            //playerAnim.SetBool("IsSliding", false);
            //szybciej spadamy
            playerRB.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        //dostosowujemy wysokoœæ skoku do czasu trzymania spacji
        else if (playerRB.velocity.y > 0 && !Input.GetButtonDown("Jump"))
        {
            playerRB.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            shouldJump = false;

        }


        if (Input.GetKey(KeyCode.Space) && shouldJump == true)
        {
            if (jumpTimeCounter > 0)
            {
                playerAnim.SetBool("IsJumping", true);
                playerAnim.SetBool("IsSliding", false);
                playerRB.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                shouldJump = false;
            }

        }
    }

    private void CheckAxis()
    {
        horizontalAxis = Input.GetAxisRaw("Horizontal");
        isJumping = Input.GetButtonDown("Jump");
        //isJumpingLow = Input.GetKey(KeyCode.Space);
    }

    public void Move(float horizontal, Rigidbody2D rb, float speed)
    {
        //poruszanie
        rb.velocity = new Vector2(horizontal * speed * Time.fixedDeltaTime, rb.velocity.y);
        if (rb.velocity.x != 0)
        {
            playerAnim.SetBool("IsRunning", true);
        }
        if (rb.velocity == Vector2.zero)
        {
            playerAnim.SetBool("IsRunning", false);
        }
        

        //je¿eli siê gibiemy
        if (isSliding)
        {
            rb.velocity = new Vector2(slideDirection * statistics.slideSpeed * Time.fixedDeltaTime, rb.velocity.y);
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
        playerAnim.SetBool("IsSliding", true);
        SFXManager.PlaySound(SFXManager.Sound.Slide, transform.position);
        playerRB.velocity += Vector2.up * Physics2D.gravity.y * (80) * Time.deltaTime;
        mainCollider.enabled = false;
        slideCollider.enabled = true;
        wallCheck.SetActive(false);
        isSliding = true;
        StartCoroutine("stopSlide");
    }
    IEnumerator stopSlide()
    {
        yield return new WaitForSeconds(0.8f);
        playerAnim.SetBool("IsSliding", false);
        mainCollider.enabled = true;
        slideCollider.enabled = false;
        wallCheck.SetActive(true);
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
        PlayerBubbleTransform.transform.localScale = new Vector3(PlayerBubbleTransform.transform.localScale.x * -1, PlayerBubbleTransform.transform.localScale.y, PlayerBubbleTransform.transform.localScale.z);
        slideDirection *= -1;
    }

    public void SpeedBoost()
    {
        statistics.moveSpeed *= 2.5f;
        StartCoroutine("speedBoostTimer");
    }
    public void JumpBoost()
    {
        isOnJumpBoost = true;
        Debug.Log(jumpForce);
        jumpForce = 25f;
        Debug.Log(jumpForce);
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
        jumpForce = basejumpForce;
        isOnJumpBoost = false;
        Debug.Log(jumpForce);
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
        if (collision.gameObject.tag == "JumpBoost" && !isOnJumpBoost)
        {
            JumpBoost();
            collision.GetComponent<Collect>().CollectItem();
        }
        if (collision.gameObject.CompareTag("CheckPoint"))
        {
            respawnPos = transform.position;
        }
        if (collision.gameObject.CompareTag("KillBox"))
        {
            Die();
            SM.levels[SM.currentLevelId].deathCounter++;
        }

    }
    private void PlayParticleSystem(ParticleSystem vfx)
    {
        vfx.Play();
    }
    [ContextMenu("LowerPlayerHp")]
    public void TakeCertainAmountOfHp()
    {
        playerAnim.Play("SkinnyHit");
        KnockBack(false);
        hp -= 1;
        if (hp <= 0)
        {
            Die();
        }
    }
    [ContextMenu("Kill player")]
    private void Die()
    {
        SFXManager.PlaySound(SFXManager.Sound.Death, transform.position);
        canMove = false;
        hp = 0;
        playerAnim.SetBool("IsDead", true);
        StartCoroutine(HUDManager.DyingScreen());
    }

    public void KnockBack(bool shouldKnockBackToRight)
    {
        canMove = false;
        Vector2 knockbackVelocity = new Vector2();
        if (shouldKnockBackToRight)
        {
            knockbackVelocity = new Vector2(3, 3);
        }
        else if (!shouldKnockBackToRight)
        {
            knockbackVelocity = new Vector2(-3, 3);
        }
        StartCoroutine(StartKnockabck(knockbackVelocity));
    }

    IEnumerator StartKnockabck(Vector2 knockbackVelocity)
    {
        playerRB.velocity = knockbackVelocity;
        yield return new WaitForSeconds(0.3f);
        if (!IsPlayerDead())
        {
            canMove = true;
        }
        else
        {
            playerRB.velocity = Vector2.zero;
        }    
    }
    public void Respawn()
    {
        if (IsPlayerDead())
        {
            transform.position = startingPos;
            playerAnim.SetBool("IsDead", false);
            canMove = true;
            hp = 3;
        }
        else
        {
            transform.position = respawnPos;
            playerRB.velocity = Vector2.zero;
            Time.timeScale = 1;
        }
    }

    bool IsPlayerDead()
    {
        return hp <= 0;
    }
}
