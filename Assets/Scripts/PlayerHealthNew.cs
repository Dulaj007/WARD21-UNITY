using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthNew : MonoBehaviour
{
    [Header("Player Health Settings")]
    public float currentHealth = 100f; // Default health
    public bool isDead = false; // Check if the player is dead

    [Header("Blood Effect Images")]
    public GameObject bloodImage1; // Blood image for health < 80
    public GameObject bloodImage2; // Blood image for health < 60
    public GameObject bloodImage3; // Blood image for health < 40
    public GameObject bloodImage4; // Blood image for health < 20

    private void Start()
    {
        // Ensure all blood images are initially inactive
        DeactivateBloodEffects();
    }

    /// <summary>
    /// Handles player taking damage.
    /// </summary>
    public void TakeDamage(float damage)
    {
        if (isDead) return; // Prevent taking damage if already dead

        currentHealth -= damage; // Reduce health by the damage amount
        currentHealth = Mathf.Clamp(currentHealth, 0, 100); // Clamp health between 0 and 100
        Debug.Log("Player took damage: " + damage + ", Remaining health: " + currentHealth); // Log the current health

       
        UpdateBloodEffects(); // Update blood effect visuals

        if (currentHealth <= 0)
        {
            Die(); // Handle player death
        }
    }

    /// <summary>
    /// Updates the blood effect visuals based on the player's current health.
    /// </summary>
    private void UpdateBloodEffects()
    {
        // Deactivate all blood images first
        DeactivateBloodEffects();

        // Activate images based on current health
        if (currentHealth < 80) bloodImage1.gameObject.SetActive(true);
        if (currentHealth < 60) bloodImage2.gameObject.SetActive(true);
        if (currentHealth < 40) bloodImage3.gameObject.SetActive(true);
        if (currentHealth < 20) bloodImage4.gameObject.SetActive(true);
    }

    /// <summary>
    /// Deactivates all blood effect images.
    /// </summary>
    private void DeactivateBloodEffects()
    {
        if (bloodImage1) bloodImage1.gameObject.SetActive(false);
        if (bloodImage2) bloodImage2.gameObject.SetActive(false);
        if (bloodImage3) bloodImage3.gameObject.SetActive(false);
        if (bloodImage4) bloodImage4.gameObject.SetActive(false);
    }

    /// <summary>
    /// Simulates a flashing effect when the player takes damage.
    /// </summary>
  

    /// <summary>
    /// Handles player death.
    /// </summary>
    private void Die()
    {
        isDead = true;
        Debug.Log("Player has died.");
        // Add logic for player death (e.g., disable movement, show death screen, restart game, etc.)
    }
}
