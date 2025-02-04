using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Transform player;               // The player target
    private float attackRange = 1.5f;       // Range within which the zombie can attack the player
    public float detectionRange = 200.0f;  // Range within which the zombie starts chasing the player
    public float damageAmount = 15.0f;     // Damage amount per attack
    public float attackCooldown = 1.0f;    // Time in seconds between attacks

    private NavMeshAgent navMeshAgent;     // NavMeshAgent component for movement
    private Animator animator;             // Animator component for animations
    private bool isAttacking = false;      // Flag to track if the zombie is attacking
    private bool isDamaged = false;        // Flag to track if the zombie is currently damaged
    private float lastAttackTime;          // Time of the last attack

    private Vector3 startingPosition;      // The zombie's starting position
    private bool isReturning = false;      // Flag to track if the zombie is returning to its starting position
    
    public ZombieSouns ZombieSounds;
    // Animator parameter hashes
    private static readonly int IdleParameter = Animator.StringToHash("Idle");
    private static readonly int WalkParameter = Animator.StringToHash("Walk");
    private static readonly int AttackParameter = Animator.StringToHash("Attack");
    private static readonly int DamageParameter = Animator.StringToHash("Damage");

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component not found on " + gameObject.name);
            return;
        }

        if (!navMeshAgent.isOnNavMesh)
        {
            Debug.LogWarning("NavMeshAgent is not on a valid NavMesh surface.");
        }
        
          


         // Initialize to idle state
   
        animator.SetBool(IdleParameter, true);
        animator.SetBool(WalkParameter, false);
        animator.SetBool(AttackParameter, false);
        animator.SetBool(DamageParameter, false);

    Debug.Log("Zombie initialized in Idle state at starting position.");

        // Start coroutine to delay setting the starting position
        StartCoroutine(SetStartingPositionAfterDelay(1.0f));


    }

     private IEnumerator SetStartingPositionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        startingPosition = transform.position;
    
        Debug.Log("Zombie's starting position set after delay: " + startingPosition);
    }

    void Update()
    {
       
        if (navMeshAgent == null || !navMeshAgent.isOnNavMesh) return;

        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        

      

        if (distanceToPlayer <= attackRange)
        {
            // Within attack range
            if (!isAttacking)
            {
                animator.SetBool(AttackParameter, true);
                animator.SetBool(WalkParameter, false);
                animator.SetBool(IdleParameter, false);
                navMeshAgent.isStopped = true; // Stop the agent to attack
                isAttacking = true;
                Debug.Log("Zombie started attacking");
            }
        }
        else
        {
            // Out of attack range
            if (isAttacking)
            {
                animator.SetBool(AttackParameter, false);
                animator.SetBool(WalkParameter, false);
                isAttacking = false;
                navMeshAgent.isStopped = false; // Resume movement
                Debug.Log("Zombie stopped attacking");
            }

            if (distanceToPlayer <= detectionRange)
            {
                // Within detection range but not in attack range, follow the player
            if (ZombieSounds != null )
            {
                ZombieSounds.PlayScrem();
            }
                animator.SetBool(WalkParameter, true);
                animator.SetBool(IdleParameter, false);
                navMeshAgent.isStopped = false; 
                navMeshAgent.SetDestination(player.position);
                isReturning = false;
            }
            else
            {
                // Out of detection range, return to starting position
                if (!isReturning)
                {
           
                    navMeshAgent.SetDestination(startingPosition);
                    animator.SetBool(WalkParameter, true);
                    animator.SetBool(IdleParameter, false);
                    isReturning = true;
                }

                // Check if the zombie has reached its starting position
                if (Vector3.Distance(transform.position, startingPosition) <= navMeshAgent.stoppingDistance)
                {
                    animator.SetBool(WalkParameter, false);
                    animator.SetBool(IdleParameter, true);
                    navMeshAgent.isStopped = true;
                    isReturning = false;
                    if (ZombieSounds != null )
                {
                    ZombieSounds.StopScrem();
                }
                    Debug.Log("Zombie returned to starting position");
                }
                else
{
    navMeshAgent.isStopped = false; // Ensure movement continues smoothly
}
            }
        }
    }

    // Method to handle taking damage (called by ZombieHealth script)
    public void HandleDamage()
    {
        if (isAttacking)
        {
            // Temporarily interrupt attack animation to play damage animation
            animator.SetBool(AttackParameter, false);
            isAttacking = false;
        }
       


        animator.SetBool(DamageParameter, true);
        isDamaged = true;

        // Reset the damage flag after a short delay (adjust as needed)
        Invoke(nameof(ResetDamage), 0.5f);
    }

    // Reset damage state
    private void ResetDamage()
    {
        animator.SetBool(DamageParameter, false);
        isDamaged = false;

        // Resume previous behavior after damage animation
        if (!isAttacking)
        {
            animator.SetBool(AttackParameter, false);
        }
    }

    // Method to detect if the zombie hits the player during attack (needs a trigger collider setup)
    private void OnTriggerStay(Collider other)
    {
        if (isAttacking && other.CompareTag("Player"))
        {
            // Ensure attack is only processed if cooldown time has passed
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                lastAttackTime = Time.time; // Update the last attack time
                Debug.Log("Zombie hit the player");
                other.GetComponent<PlayerHealth>()?.TakeDamage(damageAmount);
            }
        }
    }
}
