using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieHealth : MonoBehaviour
{
    public int maxHealth = 100;               // Maximum health of the zombie
    public int currentHealth;                // Current health of the zombie
    public Animator animator;                 // Reference to the Animator component for animations
    public ParticleSystem destructionEffect;  // Effect to play when the zombie is destroyed
    public AudioClip hitSound;                // Sound to play when the zombie is hit
    private AudioSource audioSource;          // Audio source to play the sound
    public bool isDead = false;              // Flag to check if the zombie is dead

    private static readonly string DieParameter = "Die";
    private static readonly float DeathDelay = 15f; // Delay before the zombie disappears after dying
    public float smoothMoveDuration = 0.0001f;    // Duration for the smooth transition

    public float smoothMoveLength = -1.5f; 
 public ZombieSouns ZombieSounds;
    void Start()
    {
        // Initialize the current health to the maximum health
        currentHealth = maxHealth;

        // Add an AudioSource component if not already present
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Method to take damage
    public void TakeDamage(int damageAmount)
    {
        if (isDead) return;

        // Reduce the current health by the damage amount
        currentHealth -= damageAmount;
        Debug.Log("Zombie took damage: " + damageAmount + ", Remaining health: " + currentHealth);

        // Play the hit sound if available
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

         // Trigger the damage animation
        if (animator != null)
        {
            //animator.SetBool(ZombieDamagedParameter, true); // Set the Damage parameter to true
            GetComponent<ZombieAI>()?.HandleDamage();
        }

        // Check if the health has dropped to zero or below
        if (currentHealth <= 0)
        {
                if (ZombieSounds != null )
                {
                    ZombieSounds.StopScrem();
                }
            Die();
        }
    }

    // Method to handle death
    public void Die()
    {
        isDead = true;

        // Play the dying animation
        if (animator != null)
        {
            animator.SetBool(DieParameter, true);
            Debug.Log("Zombie died.");
        }

        // Start the coroutine with a delay before the smooth transition
        StartCoroutine(DelayedSmoothMoveToYPosition(smoothMoveLength, smoothMoveDuration, 0.5f));

        // Disable the NavMeshAgent
        NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent != null)
        {
            navMeshAgent.enabled = false;
        }

        // Disable the zombie's collider to prevent further interactions
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // Destroy the zombie after a delay to let the death animation play
        Destroy(gameObject, DeathDelay);

        // Play the destruction effect if available
        if (destructionEffect != null)
        {
            ParticleSystem effectInstance = Instantiate(destructionEffect, transform.position, transform.rotation);
            Destroy(effectInstance.gameObject, effectInstance.main.duration);
        }
    }

    // Coroutine to add a delay before smoothly moving to the target Y position
    private IEnumerator DelayedSmoothMoveToYPosition(float targetY, float duration, float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Now smoothly move the zombie to the target position
        Vector3 startPosition = transform.position; // Current position
        Vector3 targetPosition = new Vector3(startPosition.x, targetY, startPosition.z); // Target position

        float elapsedTime = 0f; // Time passed

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime; // Increment time
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration); // Smooth transition
            yield return null; // Wait for the next frame
        }

        // Ensure the final position is exactly at the target position
        transform.position = targetPosition;
    }
}
