using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Maximum health
    public float currentHealth;    // Current health
    public GameObject deathUI; 

    public KeyCode healingKey = KeyCode.H;

    [Header("Blood Effect Images")]
    public GameObject bloodImage1; // Blood image for health < 80
    public GameObject bloodImage2; // Blood image for health < 60
    public GameObject bloodImage3; // Blood image for health < 40
    public GameObject bloodImage4; // Blood image for health < 20

    public GameObject UiInjection01;
    public GameObject UiInjection02;
    public GameObject UiInjection03;

    public AudioClip deathSound;             // Sound to play on death
    private AudioSource audioSource;         // Audio source for playing sounds


    public GameObject health1;               // Inventory slot for the first health item
    public GameObject health2;               // Inventory slot for the second health item
    public GameObject health3;               // Inventory slot for the third health item

               // Message to display when picking up health items


    public GameObject NoHelthItemsText;

    public bool isDead = false;             // Flag to check if player is dead
    private float healCooldown = 3f;         // Cooldown time for healing
    private bool canHeal = true;             // Flag to check if player can heal

    private HealingArmAnimation healingArmAnimation;
    
    [SerializeField] private Pistol pistolAnimation;

    private void Start()
    {
       
        helthMax();
        audioSource = GetComponent<AudioSource>();


        healingArmAnimation = FindObjectOfType<HealingArmAnimation>(); 
        pistolAnimation = FindObjectOfType<Pistol>(); 


     
           DeactivateBloodEffects();
    }

    void Update(){
        if (Input.GetKeyDown(healingKey)){
            UseHealthItem();
        }
  

    }



    // Method to take damage
    public void TakeDamage(float damage)
    {
        if (isDead) return; // Don't take damage if already dead

        currentHealth -= damage; // Reduce health by the damage amount
        Debug.Log("Player took damage: " + damage + ", Remaining health: " + currentHealth); // Log current health


          UpdateBloodEffects(); // Update blood effect visuals

      
        if (currentHealth <= 0)
        {
            Die(); // Handle player death
        }
    }

    // Coroutine to flash player when taking damage


    // Method to handle player death
    private void Die()
    {
        isDead = true; // Mark the player as dead

        if (deathSound != null)
        {
            audioSource.PlayOneShot(deathSound); // Play death sound
        }
        
       // Pause the game
    Time.timeScale = 0f;

    // Activate the Death UI
    if (deathUI != null)
    {
        deathUI.SetActive(true);
    }

    // Optionally disable player controls or other logic related to player death
    Debug.Log("Player has died. Game paused and Death UI displayed.");
    }

    // Method to restore health (e.g., when picking up health packs)
    public void RestoreHealth(float amount)
    {
       // if (isDead) return; // Can't restore health if already dead

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth); // Restore health and clamp it to maxHealth
        Debug.Log("Player helth restored" + currentHealth );
      
    }



    // Method to use a health item


    public void UseHealthItem()
{
    if (canHeal && (health1.activeSelf || health2.activeSelf || health3.activeSelf))
    {
        Debug.Log("Player can use helth items " );
        UseHealthItemCoroutine();
         
    }
    else if (!health1.activeSelf && !health2.activeSelf && !health3.activeSelf)
    {
         Debug.Log("Player can not use helth items " );
        NoHelthItemsText.SetActive(true);
       
    }
}

private void UseHealthItemCoroutine()
{
 
    if (pistolAnimation == null)
    {
    pistolAnimation = FindObjectOfType<Pistol>();
    Debug.Log("Pistol reference reassigned dynamically before calling the method");

    Debug.Log("calling pistal for health");
            pistolAnimation.TurnOnHealAnimation(); // Turn ON the healing animation
    }
    else{
        Debug.Log("calling pistal for health");
            pistolAnimation.TurnOnHealAnimation(); // Turn ON the healing animation
    }
      
     if (healingArmAnimation != null) 
        {
            Debug.Log("calling heal arm for health");
            healingArmAnimation.TurnOnHealAnimation(); // Turn ON the healing animation
             
        }

   
    if (health3.activeSelf & UiInjection03.activeSelf)
    {
        health3.SetActive(false);
        UiInjection03.SetActive(false);
    }
    else if (health2.activeSelf & UiInjection02.activeSelf)
    {
        health2.SetActive(false);
        UiInjection02.SetActive(false);
    }
    else if (health1.activeSelf & UiInjection01.activeSelf)
    {
        health1.SetActive(false);
        UiInjection01.SetActive(false);
    }



    RestoreHealth(50); // Increase health by 50
    UpdateBloodEffectsWhenHeal();
    StartCoroutine(HealCooldown()); // Start the heal cooldown

}


    // Coroutine for heal cooldown
    private IEnumerator HealCooldown()
    {
        yield return new WaitForSeconds(3f);
        if (healingArmAnimation != null) 
            {
            healingArmAnimation.TurnOffHealAnimation(); // Turn ON the healing animation
            }
        if (pistolAnimation != null) 
        {
            pistolAnimation.TurnOffHealAnimation(); // Turn ON the healing animation
             
        }
        canHeal = false; // Disable healing
        yield return new WaitForSeconds(5f);
        canHeal = true; // Enable healing
    }

 
    /// Updates the blood effect visuals based on the player's current health.
    public void UpdateBloodEffects()
    {
        // Deactivate all blood images first
       // DeactivateBloodEffects();

        // Activate images based on current health
        if (currentHealth < 80) bloodImage1.gameObject.SetActive(true);
        if (currentHealth < 60) bloodImage2.gameObject.SetActive(true);
        if (currentHealth < 40) bloodImage3.gameObject.SetActive(true);
        if (currentHealth < 20) bloodImage4.gameObject.SetActive(true);
    }

        public void UpdateBloodEffectsWhenHeal()
    {
       
        // Activate images based on current health
        if (currentHealth > 80) bloodImage4.gameObject.SetActive(false);
        if (currentHealth > 60) bloodImage3.gameObject.SetActive(false);
        if (currentHealth > 40) bloodImage2.gameObject.SetActive(false);
        if (currentHealth > 20) bloodImage1.gameObject.SetActive(false);
    }

    /// Deactivates all blood effect images.
    public void DeactivateBloodEffects()
    {
        if (bloodImage1) bloodImage1.gameObject.SetActive(false);
        if (bloodImage2) bloodImage2.gameObject.SetActive(false);
        if (bloodImage3) bloodImage3.gameObject.SetActive(false);
        if (bloodImage4) bloodImage4.gameObject.SetActive(false);
        if (UiInjection01) UiInjection01.gameObject.SetActive(false);
        if (UiInjection01) UiInjection02.gameObject.SetActive(false);
        if (UiInjection01) UiInjection03.gameObject.SetActive(false);
    }
    public void helthMax(){
         currentHealth = maxHealth;

    }
}


