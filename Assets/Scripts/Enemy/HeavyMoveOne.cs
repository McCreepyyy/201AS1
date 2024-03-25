using UnityEngine;

public class HeavyMoveOne : MonoBehaviour
{
    [SerializeField] float m_speed = 3.0f;
    [SerializeField] float m_jumpForce = 7.5f;
    [SerializeField] float m_range = 3f; // Range within which the enemy will move

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_Bandit m_groundSensor;
    private bool m_grounded = false;
    private bool m_combatIdle = false;
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
    }

    void Update()
    {
        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
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