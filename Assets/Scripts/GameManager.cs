using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // Flag to determine if the game is paused
    private bool isGamePaused = false;
    // Delay before freezing the game after player's death
    public float freezeDelay = 1.5f;

    void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Player's death event
        Health playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.OnDeath += HandlePlayerDeath;
        }
    }

    void Update()
    {
        // Check if player is dead
        if (isGamePaused)
        {
            // Freeze the game
            Time.timeScale = 0f;
        }
        else
        {
            // Unfreeze the game
            Time.timeScale = 1f;
        }
    }

    // Method to pause or unpause the game
    public void SetGamePaused(bool paused)
    {
        isGamePaused = paused;
    }

    // Method to handle player death
    void HandlePlayerDeath()
    {
        // Freeze the game after a delay when the player dies
        StartCoroutine(FreezeGameAfterDelay(freezeDelay));
    }

    IEnumerator FreezeGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetGamePaused(true);
    }
}
