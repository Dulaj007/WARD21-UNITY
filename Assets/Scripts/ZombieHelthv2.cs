using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieHealthv2 : MonoBehaviour
{
    public int maxHealth = 100;               // Maximum health of the zombie
    public int currentHealth;                // Current health of the zombie
    public Animator animator;                 // Reference to the Animator component for animations

    public AudioClip hitSound;                // Sound to play when the zombie is hit
    private AudioSource audioSource;          // Audio source to play the sound
    public bool isDead = false;              // Flag to check if the zombie is dead

    private static readonly string DieParameter = "Die";
    private static readonly float DeathDelay = 15f; // Delay before the zombie disappears after dying
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


        if (animator != null)
        {

            GetComponent<ZombieAIv2>()?.HandleDamage();
        }

        // Check if the health has dropped to zero or below
        if (currentHealth <= 0)
        {
            if (ZombieSounds != null)
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
        StartCoroutine(DelayedSmoothMoveToYPosition(0.5f));

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


    }

    // Coroutine to add a delay before smoothly moving to the target Y position (target Y is dead position)
    private IEnumerator DelayedSmoothMoveToYPosition(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);
    }

}
