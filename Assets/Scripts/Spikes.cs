using UnityEngine;

public class Spike : MonoBehaviour
{
    // Damage Inflicted
    public int damageAmount = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if object colliding is the player
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();

            // Check for health component and inflict damage on
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}
