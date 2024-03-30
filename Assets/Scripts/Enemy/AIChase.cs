using UnityEngine;

public class AIChase : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float chaseRange = 5f; // Maximum chase distance
    public Animator animator;
    public Bandit bandit; // Reference Bandit script

    private bool isChasing = false; // Flag to track whether the enemy is currently chasing the player
    private bool isDead = false; // Flag to track whether the enemy is dead

    void Update()
    {
        if (!isDead) // Check if the enemy is not dead
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);

            if (distance <= chaseRange)
            {
                isChasing = true; // Set chasing flag
                animator.SetBool("IsRunning", true); // Set the "IsRunning" parameter in the Animator

                // Move the enemy towards the player
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

                // If the player is moving, notify the Bandit to jump
                if (player.GetComponent<Rigidbody2D>().velocity.magnitude > 0 && bandit != null)
                {
                    bandit.InitiateJump();
                }
            }
            else
            {
                isChasing = false; // Reset chasing flag
                animator.SetBool("IsRunning", false); // Set the "IsRunning" parameter in the Animator
            }
        }
    }

    // Method to handle enemy death
    public void Die()
    {
        isDead = true;
        Destroy(gameObject);
    }
}