using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpUpTime;
    [SerializeField] private float jumpDistance;
    [SerializeField] private float fallGravityMultiplier;
    [SerializeField] private float wallSlideMultiplier;
    [SerializeField] private float wallJumpDistance;
    [SerializeField] private TriggerCount groundCheck;
    [SerializeField] private int numberJumps;
    [SerializeField] private UnityEvent OnJump;
    [SerializeField] private TriggerCount wallCheck;
    [SerializeField] private LayerMask wallLayers;
    [SerializeField] private float maxVelocityMultiplier;
    [SerializeField] private float reactionTime;
    [SerializeField] private UnityEvent<float> OnContact;

    private Rigidbody2D rb2;
    private Vector2? dirToWall;
    private Vector2 velocity;
    private float jumpVelocity;
    private float runVelocity;
    private float gravityUp;
    private float gravityDown;
    private float gravityWall;
    private int numberJumpsSinceGrounded;
    private bool onGround;
    private bool onWall;
    private Transform tr;
    private bool wallJumping;
    private float wallJumpTime;
    private float timeLeftGround;
    private float timeHitJump;
    private Vector2 lastSpeed = new Vector2();

    private Vector2 platformVelocity = new Vector2();
    private PlatformMover platMover;

    private void Awake()
    {
        rb2 = GetComponent<Rigidbody2D>();
        velocity = new Vector2();
        tr = transform;
        wallJumping = false;       
        CalculateConstants();
    }

    private void Start()
    {
        onGround = (groundCheck.NumberOfObjects > 0);
        onWall = (wallCheck.NumberOfObjects > 0);
        timeHitJump = -10f*reactionTime;
    }

    public void HitWall()
    {
        onWall = true;
        numberJumpsSinceGrounded = 0;
        OnContact?.Invoke(lastSpeed.x);
    }

    public void LeftWall()
    {
        onWall = false;
        numberJumpsSinceGrounded = 1;
    }

    public void HitGround()
    {
        onGround = true;
        numberJumpsSinceGrounded = 0;
        OnContact?.Invoke(lastSpeed.y);
    }

    public void LeftGround()
    {
        onGround = false;
        timeLeftGround = Time.time;
        numberJumpsSinceGrounded = 1;
        
    }

    private void CalculateConstants()
    {
        jumpVelocity = (2.0f * jumpHeight) / jumpUpTime;

        gravityUp = (jumpVelocity / jumpUpTime) / Mathf.Abs(Physics2D.gravity.y);
        gravityDown = fallGravityMultiplier * gravityUp;
        gravityWall = wallSlideMultiplier * gravityUp;

        float fallTime = Mathf.Sqrt((2.0f * jumpHeight) / Mathf.Abs(Physics2D.gravity.y * gravityDown));

        runVelocity = jumpDistance / (fallTime + jumpUpTime);

        wallJumpTime = wallJumpDistance / runVelocity;

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.W))
        {
            timeHitJump = Time.time;         
        }

        if ((Time.time - timeHitJump) < reactionTime)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        CheckAndSetGravity();
        
        lastSpeed.x = Mathf.Abs(rb2.velocity.x) / runVelocity;
        lastSpeed.y = Mathf.Abs(rb2.velocity.y) / (maxVelocityMultiplier * jumpVelocity);
    }
    private void Jump()
    {

        if (!onGround)
        {
            //First jump
            if ((Time.time - timeLeftGround) < reactionTime)
            {
                numberJumpsSinceGrounded = 0;
            }
            //Second jump
            if (numberJumpsSinceGrounded >= numberJumps) return;
        }

        numberJumpsSinceGrounded++;

        if (onWall)
        {
            dirToWall = wallCheck.GetDirectionToColliders(tr);
            velocity = rb2.velocity;
            velocity.y = jumpVelocity;

            if (dirToWall.HasValue)
            {        
                velocity.x = -1f * Mathf.Sign(dirToWall.Value.x) * runVelocity;
                StartWallJump(); 
            }

            rb2.velocity = velocity;
        }
        else
        {
            velocity = rb2.velocity;
            velocity.y = jumpVelocity;
            rb2.velocity = velocity;
        }
        
        OnJump?.Invoke();
        timeHitJump = 0f;
    }

    private void Move()
    {
        if (wallJumping) return;
        velocity = rb2.velocity;
        velocity.x = Input.GetAxis("Horizontal") * runVelocity;

        velocity.y = Mathf.Clamp(velocity.y, -1f * maxVelocityMultiplier * jumpVelocity,
            maxVelocityMultiplier * jumpVelocity);

        CalculatePlatformVelocity();
        velocity.x += platformVelocity.x;
        

        rb2.velocity = velocity;
    }


    private void CalculatePlatformVelocity()
    {
        platformVelocity.Set(0f, 0f);

        for (int i = 0; i < groundCheck.NumberOfObjects; i++)
        {
            platMover = groundCheck.ObjectsInTrigger[i].GetComponent<PlatformMover>();

            if (platMover)
            {
                platformVelocity.x += platMover.velocity.x;
                platformVelocity.y += platMover.velocity.y;
            }

        }
    }

    private void StartWallJump()
    {
        wallJumping = true;
        Invoke("EndWallJump", wallJumpTime);
    }

    private void EndWallJump()
    {
        wallJumping = false;
    }

    private void CheckAndSetGravity()
    {
        if (onWall)
        {
            rb2.gravityScale = gravityWall;
            return;
        }

        if (rb2.velocity.y < 0.0f)
        {
            rb2.gravityScale = gravityDown;
        }
        else
        {
            rb2.gravityScale = gravityUp;
        }
    }
}
