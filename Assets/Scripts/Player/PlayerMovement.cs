using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool canMove = true;
    public bool allowCharge = false;
    public bool allowSmash = false;
    public float chargeSpeed = 700;
    public float slideSpeed = 500;
    public PlayerStats statistics = new PlayerStats();
    //particle system
    public ParticleSystem slide;
    private ParticleSystem.EmissionModule slideEmission;
    public ParticleSystem smash;
    public ParticleSystem charge;
    public ParticleSystem falling;
    public ParticleSystem fakeFloorBlowUp;
    public ParticleSystem fakeWallBlowUp;
    public ParticleSystem jumpAndLand;
    public bool KonamiMoonWalk;
    //move variables
    public float horizontalAxis;
    public float moveSpeed;
    public bool facingRight = true;
    public bool isAirborn;

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
    public bool isGrounded;
    private bool isGroundedWithoutOffset;
    private bool shouldJump;
    public bool isSliding;
    public Rigidbody2D playerRB;
    public float jumpBuffer = 0.1f;
    public float coyoteetime = 0.2f;
    private float coyoteeTimeCounter;
    private float jumpBufferCounter;
    public CapsuleCollider2D mainCollider;
    public CircleCollider2D slideCollider;
    public CircleCollider2D slideCollider2;
    public GameObject SmashCheck;
    public Transform slideCheckUp;
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
    private bool isDying;
    private bool holdingSpace;
    public bool GodMode = false;
    public float GodModeTimer;
    private float _godModeTime;
    private bool isAfterSlide;

    private void Awake()
    {
        playerAnim = GameObject.FindGameObjectWithTag("PlayerSprite").GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody2D>();
        HUDManager = FindObjectOfType<HUDManager>();
    }
    private void Start()
    {
        _godModeTime = GodModeTimer;
        coyoteeTimeCounter = coyoteetime;
        slideEmission = slide.emission;
        statistics.playerPosition = gameObject.transform;
        respawnPos = transform.position;
        startingPos = transform.position;
        basejumpForce = jumpForce;
        SM = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>();
        //SM.levels[SM.currentLevelId].levelName = "Level_" + SM.currentLevelId;
    }
    private void Update()
    {
        if (!canMove)
        {
            playerRB.velocity = new Vector2(0, -5);
            playerAnim.SetBool("IsJumping", false);
            playerAnim.SetBool("IsFalling", false);
            playerAnim.SetBool("IsRunning", false);
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        isGroundedWithoutOffset = Physics2D.OverlapCircle(groundCheck.position, 0.2f, whatIsGround);
        CheckInputs();
        if (isGrounded)
        {
            playerAnim.SetBool("IsFalling", false);
        }
        if ((Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.C)) && canMove)
        {
            if (!isSliding)
            {
                Slide();
            }
        }

        ShowSlideParticles();

        if (Input.GetKeyDown(KeyCode.Tab) && canMove && allowCharge)
        {
            if (!isCharging)
            {
                Charge();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && playerRB.velocity.y != 0 && allowSmash)
        {
            if (!isSmashing)
            {
                Smash();
            }
        }

        CheckCoyoteeTime();

        if (isGroundedWithoutOffset)
        {
            playerAnim.SetBool("IsJumping", false);
            playerAnim.SetBool("IsFalling", false);
        }
        PlaySmashParticle();
        CalculateJumpBuffer();

        if (horizontalAxis > 0 && !facingRight || horizontalAxis < 0 && facingRight)
        {
            Flip();
        }

        if (isAfterSlide)
        {
            if (!Physics2D.OverlapCircle(slideCheckUp.position, 0.5f, whatIsGround))
            {
                overSlide();
                isAfterSlide = false;
            }
        }
    }

    public IEnumerator GodModeOff()
    {
        yield return new WaitForSeconds(GodModeTimer);
        GodMode = false;
    }
    public void GodModeOn()
    {
        GodMode = true;
        StartCoroutine(GodModeOff());
    }

    private void FixedUpdate()
    {
        if (!canMove) return;
        Jump();
        Move(horizontalAxis, playerRB, moveSpeed);
        RunWalkAnimation(playerRB);
        if (isSliding)
            Slide(playerRB);
        if (isCharging)
            Charge(playerRB);
    }
    private void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        Gizmos.DrawWireSphere(slideCheckUp.position, 0.5f);
    }
    #region Jump
    private void CalculateJumpBuffer()
    {
        if (Input.GetKeyDown("space"))
        {
            jumpBufferCounter = jumpBuffer;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
    }
    private void CheckCoyoteeTime()
    {
        if (isGrounded && !isAirborn)
        {
            coyoteeTimeCounter = coyoteetime;
        }
        else
        {
            coyoteeTimeCounter -= Time.deltaTime;
        }
    }
    private void Jump()
    {
        //if is player trying to jump
        if ((coyoteeTimeCounter > 0 && jumpBufferCounter > 0 && !shouldJump && !isAirborn) || (KonamiMoonWalk && holdingSpace))
        {
            jumpBufferCounter = 0;
            coyoteeTimeCounter = 0;
            isAirborn = true;
            mainCollider.enabled = true;
            SFXManager.PlaySound(SFXManager.Sound.Jump, transform.position);
            PlayParticleSystem(jumpAndLand);
            playerAnim.SetBool("IsSliding", false);
            shouldJump = true;
            jumpTimeCounter = jumpTime;
            playerRB.velocity = Vector2.up * jumpForce;
            mainCollider.enabled = true;
        }

        //if is player falling
        if (playerRB.velocity.y < 0 && !isGrounded)
        {
            playerAnim.SetBool("IsFalling", true);
            playerAnim.SetBool("IsJumping", false);
            isAirborn = true;
            playerRB.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        //accelerate jump to player space holding time
        else if (playerRB.velocity.y > 0 && !Input.GetButtonDown("Jump"))
        {
            playerRB.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (holdingSpace && shouldJump == true)
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
    #endregion
    private void ShowSlideParticles()
    {
        if (isGroundedWithoutOffset && isSliding)
        {
            slideEmission.rateOverTime = 60f;
        }
        else
        {
            slideEmission.rateOverTime = 0;
        }
    }

    private void CheckInputs()
    {
        horizontalAxis = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyUp(KeyCode.Space))
        {
            shouldJump = false;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            holdingSpace = true;
        }
        else
        {
            holdingSpace = false;
        }
    }

    public void Move(float horizontal, Rigidbody2D rb, float speed)
    {
        rb.velocity = new Vector2(horizontal * speed * Time.fixedDeltaTime, rb.velocity.y);
    }

    private void Charge(Rigidbody2D rb)
    {
        rb.velocity = new Vector2(slideDirection * chargeSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    private void Slide(Rigidbody2D rb)
    {
        rb.velocity = new Vector2(slideDirection * slideSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    public void Slide()
    {
        playerAnim.SetBool("IsSliding", true);
        SFXManager.PlaySound(SFXManager.Sound.Slide, transform.position);
        playerRB.velocity += Vector2.up * Physics2D.gravity.y * (80) * Time.deltaTime;
        mainCollider.enabled = false;
        slideCollider.enabled = true;
        slideCollider2.enabled = true;
        wallCheck.SetActive(false);
        isSliding = true;
        StartCoroutine("stopSlide");
    }
    IEnumerator stopSlide()
    {
        yield return new WaitForSeconds(0.8f);
        isAfterSlide = true;
    }

    public void overSlide()
    {
        playerAnim.SetBool("IsSliding", false);
        mainCollider.enabled = true;
        slideCollider.enabled = false;
        slideCollider2.enabled = false;
        wallCheck.SetActive(true);
        isSliding = false;
    }

    public void Charge()
    {
        playerAnim.SetBool("IsCharging", true);
        PlayParticleSystem(charge);
        isCharging = true;
        StartCoroutine("stopCharge");
    }
  
    IEnumerator stopCharge()
    {
        yield return new WaitForSeconds(0.5f);
        isCharging = false;
        playerAnim.SetBool("IsCharging", false);
    }

    public void Smash()
    {
        playerAnim.SetBool("IsSmashing", true);
        SmashCheck.SetActive(true);
        PlayParticleSystem(falling);
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
        moveSpeed *= 2.5f;
        StartCoroutine("speedBoostTimer");
    }
    public void JumpBoost()
    {
        isOnJumpBoost = true;
        jumpForce = 25f;
        StartCoroutine("jumpBoostTimer");
    }
    IEnumerator speedBoostTimer()
    {
        yield return new WaitForSeconds(3);
        moveSpeed /= 2.5f;
    }
    IEnumerator jumpBoostTimer()
    {
        yield return new WaitForSeconds(5);
        jumpForce = basejumpForce;
        isOnJumpBoost = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SpeedBoost")
        {
            SpeedBoost();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "JumpBoost")
        {
            JumpBoost();
            SFXManager.PlaySound(SFXManager.Sound.Boost, transform.position);
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
    public void PlayParticleSystem(ParticleSystem vfx)
    {
        vfx.Play();
    }
    [ContextMenu("LowerPlayerHp")]
    public void TakeCertainAmountOfHp()
    {
        if (GodMode) return;
        playerAnim.Play("SkinnyHit");
        SFXManager.PlaySound(SFXManager.Sound.GetHit, transform.position);
        KnockBack(false);
        hp -= 1;
        GodModeOn();
        if (hp <= 0)
        {
            Die();
        }
    }
    [ContextMenu("Kill player")]
    private void Die()
    {
        if (isDying) return;
        isDying = true;
        SFXManager.PlaySound(SFXManager.Sound.Death, transform.position);
        canMove = false;
        hp = 0;
        GameManager.deaths++;
        playerAnim.SetBool("IsDead", true);
        StartCoroutine(HUDManager.DyingScreen());
    }

    public void KnockBack(bool shouldKnockBackToRight, float knockBackStrength = 0)
    {
        canMove = false;
        Vector2 knockbackVelocity = new Vector2();
        if (shouldKnockBackToRight)
        {
            knockbackVelocity = new Vector2(knockBackStrength, 3);
        }
        else if (!shouldKnockBackToRight)
        {
            knockbackVelocity = new Vector2(knockBackStrength, 3);
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
            transform.position = respawnPos;
            canMove = true;
            hp = 3;
            isDying = false;
            playerAnim.SetBool("IsDead", false);
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
    private void RunWalkAnimation(Rigidbody2D rb)
    {
        if (rb.velocity.x != 0)
        {
            playerAnim.SetBool("IsRunning", true);
            if (isGrounded)
            {
                SFXManager.PlaySound(SFXManager.Sound.Step, transform.position);
            }
        }
        else if (rb.velocity == Vector2.zero)
        {
            playerAnim.SetBool("IsRunning", false);
        }
    }
    private void PlaySmashParticle()
    {
        if (shouldSmashParticle)
        {
            if (isGrounded == true && playerRB.velocity.y == 0 && isSmashing)
            {
                PlayParticleSystem(smash);
                shouldSmashParticle = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Destroyable") && isCharging)
        {
            PlayParticleSystem(fakeWallBlowUp);
            ScreenShake.Instance.Shakecamera(5f, .1f);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Smashable" && isSmashing)
        {
            PlayParticleSystem(fakeFloorBlowUp);
            ScreenShake.Instance.Shakecamera(5f, .1f);
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Destroyable") && isCharging)
        {
            PlayParticleSystem(fakeWallBlowUp);
            ScreenShake.Instance.Shakecamera(5f, .1f);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Smashable" && isSmashing)
        {
            PlayParticleSystem(fakeFloorBlowUp);
            ScreenShake.Instance.Shakecamera(5f, .1f);
            Destroy(collision.gameObject);
        }
    }

    public void SmashOver()
    {
        playerAnim.SetBool("IsSmashing", false);
    }
}