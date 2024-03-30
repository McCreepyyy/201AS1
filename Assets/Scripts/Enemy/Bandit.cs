using UnityEngine;

public class Bandit : MonoBehaviour
{
    [SerializeField] float m_jumpForce = 5.0f; // Adjust this value for the desired jump height
    [SerializeField] float m_jumpDelay = 1.5f; // Jump delay in seconds

    private Rigidbody2D m_body2d;
    private Sensor_Bandit m_groundSensor;
    private bool m_grounded = false;
    private float m_jumpTimer = 0.0f;
    private bool m_shouldJump = false;

    void Start()
    {
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
    }

    void Update()
    {
        // Update grounded state
        m_grounded = m_groundSensor.State();

        // Jump logic
        if (m_shouldJump && m_grounded)
        {
            Jump();
            m_shouldJump = false; // Reset jump flag
        }
    }

    // Public method to initiate the Bandit's jump
    public void InitiateJump()
    {
        m_shouldJump = true;
    }

    // Function to handle the Bandit's jump
    void Jump()
    {
        m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
    }
}