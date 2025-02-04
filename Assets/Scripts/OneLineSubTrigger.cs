using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneLineSubTrigger : MonoBehaviour
{
    public bool inReach = false;          // To track if the player is in range
    public AudioSource Voice;            // Audio to play when triggered
    public OneLineSubs OneLineSubs;      // Subtitles to show when triggered
    private BoxCollider triggerCollider; // Reference to the Box Collider component

    // Start is called before the first frame update
    void Start()
    {
        // Cache the reference to the Box Collider on this GameObject
        triggerCollider = GetComponent<BoxCollider>();

        if (triggerCollider == null)
        {
            Debug.LogWarning("Box Collider not found on " + gameObject.name);
        }
    }

    // Method called when another collider enters this trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inReach = true;

            if (Voice != null)
            {
                Voice.Play(); // Play the audio
            }

            if (OneLineSubs != null)
            {
                OneLineSubs.StartOneLineSub(); // Show subtitles
            }

            // Disable the trigger Box Collider to prevent re-triggering
            if (triggerCollider != null)
            {
                triggerCollider.enabled = false;
                Debug.Log("Box Collider disabled after triggering.");
            }
        }
    }
}
