using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public event Action OnDeath; // Event to be triggered when health reaches zero

    public int currentHealth = 100;
    
    // Function to reduce health
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            if (OnDeath != null)
                OnDeath(); // Trigger OnDeath event if subscribed
        }
    }
}