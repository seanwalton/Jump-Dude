using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpUpTime;
    [SerializeField] private float jumpDistance;
    [SerializeField] private float fallGravityMultiplier;
    private Rigidbody2D rb2;
    private Vector2 velocity;
    private float jumpVelocity;
    private float runVelocity;
    private float gravityUp;
    private float gravityDown;

    private void Awake()
    {
        rb2 = GetComponent<Rigidbody2D>();
        velocity = new Vector2();
        CalculateConstants();
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
    }

    private void FixedUpdate()
    {
        CheckAndSetGravity();
    }
    private void Jump()
    {     
        velocity = rb2.velocity;
        velocity.y = jumpVelocity;
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
