using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PickUpItem : MonoBehaviour
{
    public GameObject handWithItem; // Reference to the player's hand holding the item (initially disabled)
    public GameObject UIShow;
    public GameObject pickUpText; // UI text to show when the player is near the object
    public AudioSource pickUpSound; // Sound to play when the object is picked up

    public bool inReach; // Flag to check if the player is within reach to interact
   

    public OneLineSubs OneLineSubs;




    void Start()
    {
       

        if (handWithItem != null)
        {
            handWithItem.SetActive(false); // Ensure the hand with item is initially disabled
        }
        else
        {
            Debug.LogError("Hand with item (handWithItem) is not assigned in the Inspector.");
        }

        if (UIShow != null)
        {
            UIShow.SetActive(false); // Ensure the hand with item is initially disabled
        }
        else
        {
            Debug.LogError("Hand with item (UIShow) is not assigned in the Inspector.");
        }
  
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = true;
            if (pickUpText != null)
            {
                pickUpText.SetActive(true); // Show the pick-up text
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = false;
            if (pickUpText != null)
            {
                pickUpText.SetActive(false); // Hide the pick-up text
            }
        }
    }

    void Update()
    {
        // Check if the player is in reach and presses the interact key (E)
        if (inReach && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }
    }

    void PickUp() 
    {
        
        if (handWithItem != null)
        {
            // Activate
            handWithItem.SetActive(true);
          
        }
        else
        {
            Debug.LogError("Hand with item (handWithItem) is not assigned.");
        }

        if (UIShow != null)
        {
            // Activate
            UIShow.SetActive(true);
          
        }
        else
        {
            Debug.LogError("Hand with item (UIShow) is not assigned.");
        }

        // Play pick-up sound if assigned
        if (pickUpSound != null)
        {
            pickUpSound.Play();
        }

        // Hide pick-up text and deactivate the object
        if (pickUpText != null)
        {
            pickUpText.SetActive(false);
        }

      if (OneLineSubs != null)
        {
            OneLineSubs.StartOneLineSub();
        }
    
       

        gameObject.SetActive(false); // Deactivate this game object (picked up item)

        
    }
}
