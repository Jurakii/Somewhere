using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 5f;
    public KeyCode jumpKey = KeyCode.Space;
    private bool isJumping = false;
    private Animator animator;
    private Rigidbody2D rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(jumpKey) && !isJumping)
        {
            Jump();
        }

        if (rb.velocity.y > 0)  // Moving upwards
        {
            animator.Play("Up");
        }
        else if (rb.velocity.y < 0)  // Moving downwards
        {
            animator.Play("Down");
        }
        else if(!isJumping) // No vertical movement
        {
            animator.Play("Run");
        }
    }

    private void Jump()
    {
        if (rb != null)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
