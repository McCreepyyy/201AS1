using UnityEngine;

public class Heal : MonoBehaviour
{
    // Amount of health gained from pickup
    public int healAmount = 50;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if object colliding is the Player
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();

            if (playerHealth != null)
            {
                // Heal player and destroy heart object
                playerHealth.Heal(healAmount);
                Destroy(gameObject);
            }
        }
    }
}