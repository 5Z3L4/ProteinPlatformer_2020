using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementEndless : MonoBehaviour
{
    public float moveSpeed;
    private float moveSpeedStore;
    public float jumpForce;

    public float speedIncreaseMilestone;
    public float speedIncreaseMilestoneStore;
    private float speedMilestoneCount;
    private float speedMilestoneCountStore;
    public float speedMultiplier;


    public float jumpTime;
    public float jumpTimeCounter;


    public bool isGrounded;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float groundCheckRadius;

    private Rigidbody2D myRB;
    public EndlessGameManager theGameManager;
    public EndlessModeUI EndlessModeUI;
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        jumpTimeCounter = jumpTime;
        speedMilestoneCount = speedIncreaseMilestone;
        moveSpeedStore = moveSpeed;
        speedMilestoneCountStore = speedMilestoneCount;
        speedIncreaseMilestoneStore = speedIncreaseMilestone;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        myRB.velocity = new Vector2(moveSpeed, myRB.velocity.y);

        if (transform.position.x > speedMilestoneCount)
        {
            speedMilestoneCount += speedIncreaseMilestone;
            speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier;
            moveSpeed = moveSpeed * speedMultiplier;
        }
        if ((Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0)) && isGrounded)
        {
            myRB.velocity = new Vector2(myRB.velocity.x, jumpForce);
        }
        if ((Input.GetButtonUp("Jump") || Input.GetMouseButtonUp(0)) && myRB.velocity.y > 0)
        {
            myRB.velocity = new Vector2(myRB.velocity.x, myRB.velocity.y / 3f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("KillBox"))
        {
            moveSpeed = moveSpeedStore;
            EndlessModeUI.ShowDeathScreen();
            speedMilestoneCount = speedMilestoneCountStore;
            speedIncreaseMilestone = speedIncreaseMilestoneStore;
        }
    }
}
