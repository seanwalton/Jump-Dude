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
    [SerializeField] private TriggerCount groundCheck;
    [SerializeField] private int numberJumps;
    [SerializeField] private UnityEvent OnJump;

    private Rigidbody2D rb2;
    private Vector2 velocity;
    private float jumpVelocity;
    private float runVelocity;
    private float gravityUp;
    private float gravityDown;
    private int numberJumpsSinceGrounded;
    private bool onGround;

    private void Awake()
    {
        rb2 = GetComponent<Rigidbody2D>();
        velocity = new Vector2();
        CalculateConstants();
    }

    private void Start()
    {
        onGround = (groundCheck.NumberOfObjects > 0);
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

        float fallTime = Mathf.Sqrt((2.0f * jumpHeight) / Mathf.Abs(Physics2D.gravity.y * gravityDown));

        runVelocity = jumpDistance / (fallTime + jumpUpTime);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }

        Move();
    }

    private void FixedUpdate()
    {
        CheckAndSetGravity();
    }
    private void Jump()
    {

        if ((!onGround) && (numberJumpsSinceGrounded >= numberJumps)) return;

        numberJumpsSinceGrounded++;

        velocity = rb2.velocity;
        velocity.y = jumpVelocity;
        rb2.velocity = velocity;
        OnJump?.Invoke();
    }

    private void Move()
    {
        velocity = rb2.velocity;
        velocity.x = Input.GetAxis("Horizontal") * runVelocity;
        rb2.velocity = velocity;
    }

    private void CheckAndSetGravity()
    {
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
