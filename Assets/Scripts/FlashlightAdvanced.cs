using UnityEngine;

public class FlashlightToggle : MonoBehaviour
{
    // Reference to the Light component that will act as the flashlight
    private Light flashlight;

    // Boolean to track the flashlight state
    private bool isFlashlightOn = false;

    // Reference to the AudioSource component
    private AudioSource audioSource;

    // Public AudioClip fields for the on and off sounds
    public AudioSource flashlightOnSound;
    public AudioSource flashlightOffSound;

    void Start()
    {
        // Get the Light component attached to this game object
        flashlight = GetComponent<Light>();

        // Get the AudioSource component attached to this game object
        audioSource = GetComponent<AudioSource>();

        // Ensure the flashlight is off at the start
        if (flashlight != null)
        {
            flashlight.enabled = false;
        }
        else
        {
            Debug.LogError("No Light component found on this GameObject. Please attach a Light component.");
        }

        // Ensure there is an AudioSource component
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource component found on this GameObject. Please attach an AudioSource component.");
        }
    }

    void Update()
    {
        // Check if the 'F' key is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Toggle the flashlight state
            isFlashlightOn = !isFlashlightOn;

            // Enable or disable the Light component based on the state
            if (flashlight != null)
            {
                flashlight.enabled = isFlashlightOn;
            }

            // Play the appropriate sound based on the state
            if (audioSource != null)
            {
                if (isFlashlightOn && flashlightOnSound != null)
                {
                    flashlightOnSound.Play();
                }
                else if (!isFlashlightOn && flashlightOffSound != null)
                {
                    flashlightOffSound.Play();
                }
            }
        }
    }
}
