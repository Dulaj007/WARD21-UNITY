using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlashlightController : MonoBehaviour
{
    public Animator flashlightAnimator; // Reference to the Animator component
    public bool isFlashlightOn = false; // Tracks whether the flashlight is on or off
    public bool FlashTimer = false;
    public AudioSource flashlightOnSound;
    public AudioSource flashlightOffSound;
     public float WaitingTimer = 3f; 
    // Singleton instance
    public static FlashlightController Instance { get; private set; }

    void Awake()
    {
        // Initialize the Singleton instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }

    void Update()
    {
        // Check if the user presses the F key
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isFlashlightOn & FlashTimer)
            {
                FlashOff();
                FlashTimer =false;
            }
            else if(!isFlashlightOn & !FlashTimer)
            {
                FlashOn();
                
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (isFlashlightOn)
            {
               FlashTimer =true;
                StartCoroutine(ReloadWaitAnimation(WaitingTimer));
                
            }
        }
        
    }

    // Optional: Reset the bools when transitioning back to the Idle state
    public void ResetAnimationBools()
    {
        if (flashlightAnimator != null)
        {
            flashlightAnimator.SetBool("FlashOn", false);
            flashlightAnimator.SetBool("FlashOff", false);
        }
    }

    // Turn the flashlight on
    public void FlashOn()
    {
        if (flashlightAnimator != null)
        {
            flashlightAnimator.SetBool("FlashOn", true);
            flashlightAnimator.SetBool("FlashOff", false);
            isFlashlightOn = true;
            FlashTimer = true;
            flashlightOnSound.Play();

       
        }
    }

    // Turn the flashlight off
    public void FlashOff()
    {
        if (flashlightAnimator != null)
        {
            flashlightAnimator.SetBool("FlashOff", true);
            flashlightAnimator.SetBool("FlashOn", false);
            isFlashlightOn = false;
            flashlightOffSound.Play();

         
        }
    }
    
    public IEnumerator ReloadWaitAnimation(float WaitingTime){

            FlashOff();
          
                      // Wait for 2 seconds
            yield return new WaitForSeconds(WaitingTime);
            
            Debug.Log(" waiting 5 seconds.");
              FlashTimer =false;
    }
}

/*
using System.Collections;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public Light flashlight;             // The flashlight component
    public Animator handAnimator;        // Animator for the hand
    public KeyCode toggleKey = KeyCode.F; // Key to toggle the flashlight
    public KeyCode cooldownKey = KeyCode.R; // Key to start the cooldown
    public KeyCode healingKey = KeyCode.H; // Key to start the healing animation

    public GameObject heal1; // First healing item GameObject
    public GameObject heal2; // Second healing item GameObject
 
    public GameObject heal3; // Third healing item GameObject

    private bool isFlashlightOn = false; // To track the state of the flashlight arm animation
    private bool isCooldownActive = false; // To track if the cooldown is active
    private float cooldownTime = 2f;    // The cooldown duration in seconds
    private float currentCooldown = 0f;  // The current remaining cooldown time

    private float healingCooldownTime = 3.8f; // Cooldown time for healing
    private bool canUseHealing = true;   // Flag to check if healing key can be used

    void Start()
    {
        // Keep the flashlight always on
        flashlight.enabled = true;

        // Initially, the flashlight arm is off (or not showing the "on" animation)
        handAnimator.Play("flashlightNo");
    }

    void Update()
    {
        // Check for the toggle input and cooldown key
        if (Input.GetKeyDown(toggleKey) && !isCooldownActive)
        {
            ToggleFlashlight();
        }

        if (Input.GetKeyDown(cooldownKey))
        {
            HandleCooldown();
        }

        // Update the cooldown timer if it's active
        if (isCooldownActive)
        {
            UpdateCooldown();
        }

        // Check if the healing key is pressed, cooldown is over, and at least one healing item is available
        if (Input.GetKeyDown(healingKey) && canUseHealing && HasHealingItems())
        {
            // Trigger the healing animation
            handAnimator.SetBool("Heal", true);

            // Start a coroutine to reset the Heal bool after the animation plays
            StartCoroutine(ResetHealingAnimation());

            // Disable healing key usage during cooldown
            canUseHealing = false;

            // Start the healing cooldown coroutine
            StartCoroutine(HealingCooldown());
        }
    }

    void ToggleFlashlight()
    {
        // Toggle the flashlight arm state
        isFlashlightOn = !isFlashlightOn;

        // Set the FlashlightToggle parameter to control the animation transitions
        handAnimator.SetBool("FlashlightToggle", isFlashlightOn);

        // Play the appropriate animation
        if (isFlashlightOn)
        {
            handAnimator.Play("flashlightOn");
        }
        else
        {
            handAnimator.Play("flashlightOff");
        }
    }

    void HandleCooldown()
    {
        if (isFlashlightOn)
        {
            // If the flashlight arm is on, turn it off immediately
            isFlashlightOn = false;
            handAnimator.SetBool("FlashlightToggle", false);
            handAnimator.Play("flashlightOff");
        }
        else if (!isCooldownActive)
        {
            // If the flashlight arm is off and not in cooldown, start the cooldown
            isCooldownActive = true;
            currentCooldown = cooldownTime;

            // Ensure the flashlight arm stays off during the cooldown
            handAnimator.SetBool("FlashlightToggle", false);
            handAnimator.Play("flashlightNo");
        }
    }

    void UpdateCooldown()
    {
        // Reduce the cooldown timer
        currentCooldown -= Time.deltaTime;

        // Check if the cooldown has completed
        if (currentCooldown <= 0f)
        {
            isCooldownActive = false;
            currentCooldown = 0f;
        }
    }

    private IEnumerator ResetHealingAnimation()
    {
        // Wait until the animation is finished
        yield return new WaitForSeconds(handAnimator.GetCurrentAnimatorStateInfo(0).length);

        // Reset the Heal bool
        handAnimator.SetBool("Heal", false);
    }

    private IEnumerator HealingCooldown()
    {
        // Wait for the specified cooldown time
        yield return new WaitForSeconds(healingCooldownTime);

        // Enable healing key usage after cooldown
        canUseHealing = true;
    }

    // Check if any of the healing items are active (checked)
    private bool HasHealingItems()
    {
        // Return true if at least one healing item is active, otherwise false
        return (heal1 != null && heal1.activeSelf) || (heal2 != null && heal2.activeSelf) || (heal3 != null && heal3.activeSelf);
    }
}
*/
