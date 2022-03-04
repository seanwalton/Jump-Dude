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
    }

    public void HitWall()
    {
        onWall = true;
        numberJumpsSinceGrounded = 0;
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
    }

    public void LeftGround()
    {
        onGround = false;
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
            Jump();
        }
    }

    private void FixedUpdate()
    {
        CheckAndSetGravity();
    }
    private void Jump()
    {

        if ((!onGround) && (numberJumpsSinceGrounded >= numberJumps)) return;

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
    }

    private void Move()
    {
        if (wallJumping) return;
        velocity = rb2.velocity;
        velocity.x = Input.GetAxis("Horizontal") * runVelocity;

        velocity.y = Mathf.Clamp(velocity.y, -1f * maxVelocityMultiplier * jumpVelocity,
            maxVelocityMultiplier * jumpVelocity);

        rb2.velocity = velocity;
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
