using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioSource footstepsSound;
    public Rigidbody2D rb2D; // Reference to the Rigidbody2D component

    bool isGrounded = true; // Flag to track if the character is grounded

    void Update()
    {
        // Check if the character is grounded
        isGrounded = IsGrounded();

        // Check for movement input
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

        // Check if the character is grounded and moving, then enable footstep sound
        if (isGrounded && isMoving)
        {
            footstepsSound.enabled = true;
        }
        else
        {
            footstepsSound.enabled = false;
        }

        // Check for jump input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Pause or stop footstep sound when jumping
            footstepsSound.Pause(); // You can use Stop() instead if you prefer
        }
    }

    bool IsGrounded()
    {
        // Cast a ray downwards to check if the character is grounded
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        if (hit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // If using Rigidbody2D for jumping, you can detect collision with ground to re-enable footstep sound
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            footstepsSound.Play(); // Resume footstep sound
        }
    }
}