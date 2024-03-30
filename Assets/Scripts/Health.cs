using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    // Event to be triggerred when health equals 0
    public event Action OnDeath;

    public int currentHealth;
    public int maxHealth = 100;
    public float delayBeforeFreeze = 1.5f; // Delay before freezing the game
    public bool isDead = false; // Flag to track player's death state
    public HealthBar healthBar;
    public Animator playerAnimator; // Reference to the player's Animator component

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        // Get the Animator component of the player
        playerAnimator = GetComponent<Animator>();
    }

    // Function to reduce health
    public void TakeDamage(int damageAmount)
    {
        // Check if player is dead
        if (!isDead)
        {
            currentHealth -= damageAmount;
            // Update health bar
            healthBar.SetHealth(currentHealth);

            if (currentHealth <= 0)
            {
                // Set players death state to true and trigger death event
                currentHealth = 0;
                isDead = true;
                OnDeath?.Invoke();

                // Play death animation
                if (playerAnimator != null)
                {
                    playerAnimator.SetTrigger("Death");
                }

                // Delay before freezing the game
                StartCoroutine(FreezeGameAfterDelay(delayBeforeFreeze));
            }
        }
    }

    // Function to heal the player
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        // Ensure currentHealth does not exceed max health
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        healthBar.SetHealth(currentHealth); // Update health bar
    }

    System.Collections.IEnumerator FreezeGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.instance.SetGamePaused(true);
    }
}
