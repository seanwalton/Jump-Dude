using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpVelocity;
    private Rigidbody2D rb2;
    private Vector2 velocity;


    private void Awake()
    {
        rb2 = GetComponent<Rigidbody2D>();
        velocity = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
    }

    private void Jump()
    {     
        velocity = rb2.velocity;
        velocity.y += jumpVelocity;
        rb2.velocity = velocity;
    }
}
