using UnityEngine;

public class AICombat : MonoBehaviour
{
    public int damage = 10;
    public float attackRange = 2f; // Maximum distance within which the AI will attack the player
    public float attackCooldown = 1f; // Cooldown period between attacks
    public LayerMask playerLayer; // Layer mask to specify which layer the player belongs to
    public RuntimeAnimatorController animatorController; // Reference to the AI's Animator Controller

    private Animator animator; // Reference to the AI's Animator component
    private Health playerHealth; // Reference to the player's health component
    private float lastAttackTime = 0f; // Time when the last attack occurred

    void Start()
    {
        // Get the Animator component attached to the enemy
        animator = GetComponent<Animator>();

        // Set the Animator Controller if not already set
        if (animatorController != null)
        {
            animator.runtimeAnimatorController = animatorController;
        }

        // Find the player's health component
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<Health>();
            playerHealth.OnDeath += HandlePlayerDeath; // Subscribe to the player's death event
        }
    }

    private void Update()
    {
        // Check if the player is dead, do not attack if dead
        if (playerHealth != null && playerHealth.isDead)
            return;

        // Check if enough time has passed since the last attack
        if (Time.time - lastAttackTime < attackCooldown)
            return;

        // Check if the player is in attack range
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("Player"))
            {
                // If the player is in range, perform the attack
                Attack(collider.gameObject);
                // Trigger the attack animation
                animator.SetTrigger("Attack");
                // Update the last attack time
                lastAttackTime = Time.time;
            }
        }
    }

    void Attack(GameObject player)
    {
        // This method represents the attack action
        // Here, you can add any attack logic you want, such as dealing damage to the player
        Debug.Log(gameObject.name + " attacks " + player.name + ".");
        
        // Example: Deal damage to the player
        Health playerHealth = player.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a visual representation of the attack range in the Scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void HandlePlayerDeath()
    {
        // Do anything you need to handle the player's death here
    }
}

