using UnityEngine;

public class Spikes : MonoBehaviour
{
    public int damagePerSecond = 35; // Damage inflicted per second
    public float timeToDeath = 3f; // Time before player dies when on spikes
    private float timeElapsed = 0f; // Time elapsed while player is on spikes
    private bool isPlayerOnSpikes = false; // Flag to track if player is on spikes
    private Health playerHealth; // Reference to the Health component of the player

    private void Start()
    {
        // Find the Health component attached to the player
        playerHealth = FindObjectOfType<Health>();

        if (playerHealth == null)
        {
            Debug.LogError("Health component not found on the player!");
        }
    }

    private void Update()
    {
        // Check if player is on spikes
        if (isPlayerOnSpikes)
        {
            // Increment time elapsed
            timeElapsed += Time.deltaTime;

            // Deal damage every second
            if (timeElapsed >= 1f)
            {
                // Inflict damage to the player
                playerHealth.TakeDamage(damagePerSecond);
                timeElapsed = 0f; // Reset time elapsed
            }

            // Check if player's time on spikes exceeds the time to death
            if (timeElapsed >= timeToDeath)
            {
                // Handle player death here (e.g., reload scene, show game over screen, etc.)
                Debug.Log("Player died on spikes!");
                timeElapsed = 0f; // Reset time elapsed
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnSpikes = true; // Set flag indicating player is on spikes
            Debug.Log("Player entered spikes");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnSpikes = false; // Reset flag indicating player left spikes
            timeElapsed = 0f; // Reset time elapsed
            Debug.Log("Player exited spikes");
        }
    }
}
