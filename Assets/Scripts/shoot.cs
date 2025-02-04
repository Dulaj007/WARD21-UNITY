using UnityEngine;

public class ShootableObject : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health of the shootable object
    private int currentHealth;  // Current health of the shootable object

    public ParticleSystem destructionEffect; // Effect to play when the object is destroyed
    public AudioClip hitSound;   // Sound to play when the object is hit
    private AudioSource audioSource; // Audio source to play the sound

    void Start()
    {
        // Initialize the current health to the maximum health
        currentHealth = maxHealth;

        // Add an AudioSource component if not already present
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void TakeDamage(int damageAmount)
    {
        // Reduce the current health by the damage amount
        currentHealth -= damageAmount;

        // Play the hit sound if available
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        // Check if the health has dropped to zero or below
        if (currentHealth <= 0)
        {
            DestroyObject();
        }
    }

    void DestroyObject()
    {
        // Play the destruction effect if available
        if (destructionEffect != null)
        {
            ParticleSystem effectInstance = Instantiate(destructionEffect, transform.position, transform.rotation);
            Destroy(effectInstance.gameObject, effectInstance.main.duration);
        }

        // Destroy the shootable object game object
        Destroy(gameObject);
    }
}
