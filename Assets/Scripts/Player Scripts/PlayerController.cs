using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpVelocity;

    // Start is called before the first frame update
    void Start()
    {
        
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
        Rigidbody2D rb2 = GetComponent<Rigidbody2D>();
        Vector2 velocity = rb2.velocity;
        velocity.y += jumpVelocity;
        rb2.velocity = velocity;
    }
}
