using UnityEngine;

public class LightMoveTwo : MonoBehaviour
{
    [SerializeField] float m_speed = 3.0f;
    [SerializeField] float m_jumpForce = 6.0f;
    [SerializeField] float m_range = 5f; // Range within which the enemy will move
    [SerializeField] Transform m_player; // Reference to the player's Transform

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_Bandit m_groundSensor;
    private Health m_health; // Reference to the Health component
    private bool m_isDead = false;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();

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
        if (m_health.currentHealth <= 0 && !m_isDead)
        {
            m_isDead = true;
            m_animator.SetBool("Grounded", true);
    
            // Make RB Kinematic to avoid collisions
            m_body2d.isKinematic = true;
    
            Destroy(gameObject);
            return;
        }

        if (!m_isDead && m_player != null)
        {
            // Calculate the distance between the enemy and the player
            float distanceToPlayer = Vector3.Distance(transform.position, m_player.position);

            // If the player is within range, move towards the player
            if (distanceToPlayer <= m_range)
            {
                Vector3 direction = (m_player.position - transform.position).normalized;
                transform.position += direction * m_speed * Time.deltaTime;
                FlipSprite(direction.x);
                m_animator.SetInteger("AnimState", 2);
            }
        }
    }

    void FlipSprite(float directionX)
    {
        if (directionX > 0) // Move Right
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (directionX < 0) // Move Left
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}