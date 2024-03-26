using UnityEngine;

public class HeavyMoveOne : MonoBehaviour
{
    [SerializeField] float m_speed = 3.0f;
    [SerializeField] float m_jumpForce = 7.5f;
    [SerializeField] float m_range = 3f; // Range within which the enemy will move

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_Bandit m_groundSensor;
    private Health m_health; // Reference to the Health component
    private bool m_isDead = false;
    private bool m_movingRight = true;
    private Vector3 m_initialPosition;
    private Vector3 m_leftPosition;
    private Vector3 m_rightPosition;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();

        m_initialPosition = transform.position;
        m_leftPosition = m_initialPosition - Vector3.right * m_range / 2;
        m_rightPosition = m_initialPosition + Vector3.right * m_range / 2;

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

        // -- Handle automatic movement --
        if (m_movingRight)
        {
            MoveRight();
            if (transform.position.x >= m_rightPosition.x)
            {
                m_movingRight = false;
                FlipSprite();
            }
        }
        else
        {
            MoveLeft();
            if (transform.position.x <= m_leftPosition.x)
            {
                m_movingRight = true;
                FlipSprite();
            }
        }
    }

    void MoveRight()
    {
        transform.position = Vector3.MoveTowards(transform.position, m_rightPosition, m_speed * Time.deltaTime);
        m_animator.SetInteger("AnimState", 2); // Change to the correct parameter name
    }

    void MoveLeft()
    {
        transform.position = Vector3.MoveTowards(transform.position, m_leftPosition, m_speed * Time.deltaTime);
        m_animator.SetInteger("AnimState", 2); // Change to the correct parameter name
    }

    void FlipSprite()
    {
        Vector3 newScale = transform.localScale;
        newScale.x *= -1; // Flipping the sprite horizontally
        transform.localScale = newScale;
    }
}