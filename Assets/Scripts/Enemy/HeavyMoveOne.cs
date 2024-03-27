using UnityEngine;

public class HeavyMoveOne : MonoBehaviour
{
    [SerializeField] float m_speed = 3.0f;
    [SerializeField] float m_chaseRange = 5.0f; // Range within which the enemy will start chasing the player

    private Transform m_player; // Reference to the player's transform
    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Health m_health; // Reference to the Health component
    private bool m_isDead = false;
    private bool m_movingRight = true;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();

        // Find the player object by tag
        m_player = GameObject.FindGameObjectWithTag("Player").transform;
        if (m_player == null)
        {
            Debug.LogError("Player object not found!");
        }

        // Get the Health component attached to the enemy
        m_health = GetComponent<Health>();
        if (m_health == null)
        {
            Debug.LogError("Health component not found!");
        }
    }

    void Update()
    {
        // Check if the enemy is dead
        if (m_health.currentHealth <= 0)
        {
            m_isDead = true;
            m_animator.SetBool("Grounded", true); // Ensure animation state is correct

            // Make the Rigidbody kinematic when dead to prevent external forces from affecting it
            m_body2d.isKinematic = true;

            return; // Don't execute further movement logic if dead
        }

        // If the player is within chase range, start chasing
        if (Vector2.Distance(transform.position, m_player.position) <= m_chaseRange)
        {
            ChasePlayer();
        }
        else
        {
            // Otherwise, stop chasing
            StopChasing();
        }
    }

    void ChasePlayer()
    {
        // Calculate the direction towards the player
        Vector2 direction = (m_player.position - transform.position).normalized;

        // Move towards the player
        float movement = m_speed * Time.deltaTime;
        transform.Translate(direction * movement);
        m_animator.SetInteger("AnimState", 2); // Change to the correct parameter name

        // Flip sprite based on movement direction
        if (direction.x > 0 && !m_movingRight)
        {
            FlipSprite(false); // Face right
        }
        else if (direction.x < 0 && m_movingRight)
        {
            FlipSprite(true); // Face left
        }
    }

    void StopChasing()
    {
        // Implement stopping behavior if needed
    }

    void FlipSprite(bool faceLeft)
    {
        Vector3 newScale = transform.localScale;
        if (faceLeft)
        {
            newScale.x = Mathf.Abs(newScale.x) * -1; // Flip sprite to face left
        }
        else
        {
            newScale.x = Mathf.Abs(newScale.x); // Face right
        }
        transform.localScale = newScale;

        // Update moving direction
        m_movingRight = !faceLeft;
    }
}