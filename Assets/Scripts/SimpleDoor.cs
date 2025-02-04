using System.Collections;
using UnityEngine;

public class SimpleDoor : MonoBehaviour
{
    // These will now show up in the Unity Inspector
    public Animator door;            // Reference to the Animator controlling door animations
    public GameObject openText;      // UI text to prompt the player to open the door
    public KeyCode toggleKey = KeyCode.E; // Key to interact with the door
    public AudioSource Voice;
    private bool inReach = false;    // Is the player in range of the door
    private bool doorIsOpen = false; // Has the door already been opened

    void Start()
    {
        openText.SetActive(false);   // Ensure the prompt is hidden initially
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach") && !doorIsOpen) // If player is near and door is not opened
        {
            inReach = true;
            openText.SetActive(true);  // Show the "Press E to Open" text
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = false;
            openText.SetActive(false); // Hide the prompt when the player leaves the area
        }
    }

    void Update()
    {
        // If the player is in range and presses 'E' to open the door
        if (inReach && !doorIsOpen && Input.GetKeyDown(toggleKey)) 
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        door.SetBool("Open", true);  // Trigger the "Open" animation in the Animator
        if (Voice != null)
            {
                Voice.Play();
            }
        openText.SetActive(false);   // Hide the prompt
        doorIsOpen = true;           // Mark the door as opened to prevent further interaction
    }
}
