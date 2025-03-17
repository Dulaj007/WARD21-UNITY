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

    public static FlashlightController Instance { get; private set; } //Singleton instance

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
                FlashTimer = false;
            }
            else if (!isFlashlightOn & !FlashTimer)
            {
                FlashOn();

            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (isFlashlightOn)
            {
                FlashTimer = true;
                StartCoroutine(ReloadWaitAnimation(WaitingTimer));

            }
        }

    }


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

    public IEnumerator ReloadWaitAnimation(float WaitingTime)
    {

        FlashOff();

        // Wait for 2 seconds
        yield return new WaitForSeconds(WaitingTime);

        Debug.Log(" waiting 5 seconds.");
        FlashTimer = false;
    }
}
