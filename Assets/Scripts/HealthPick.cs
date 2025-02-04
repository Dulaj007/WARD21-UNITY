using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthPick : MonoBehaviour
{
    public GameObject Heal01; 
    public GameObject Heal02; 
    public GameObject Heal03; 

    public GameObject UIShow01;
     public GameObject UIShow02;
      public GameObject UIShow03;
    public GameObject pickUpText; // UI text to show when the player is near the object
    public AudioSource pickUpSound; // Sound to play when the object is picked up

    private bool inReach; // Flag to check if the player is within reach to interact
       void Start()
    {
        if (pickUpText != null)
        {
            pickUpText.SetActive(false); // Hide the pick-up text initially
     
        }

        if (Heal01 != null)
        {
            Heal01.SetActive(false); // Ensure the hand with item is initially disabled
        }
        else
        {
            Debug.LogError("Hand with item (handWithItem) is not assigned in the Inspector.");
        
       
        }
         if (Heal02 != null)
        {
            Heal02.SetActive(false); // Ensure the hand with item is initially disabled
        }
        if (Heal03 != null)
        {
            Heal03.SetActive(false); // Ensure the hand with item is initially disabled
        }

        if (UIShow01 != null)
        {
            UIShow01.SetActive(false); // Ensure the hand with item is initially disabled
        }
        else
        {
            Debug.LogError("Hand with item (handWithItem) is not assigned in the Inspector.");
        }
         if (UIShow02 != null)
        {
            UIShow02.SetActive(false); // Ensure the hand with item is initially disabled
        }
         if (UIShow03 != null)
        {
            UIShow03.SetActive(false); // Ensure the hand with item is initially disabled
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
      

        if ( Heal02.activeSelf )
        {
            // Activate
             Heal03.SetActive(true);
          
        }
        if (Heal01.activeSelf)
        {
            Heal02.SetActive(true);
         
        }
        else
        {
            Heal01.SetActive(true);
        }


        if ( UIShow02.activeSelf )
        {
            // Activate
             UIShow03.SetActive(true);
          
        }
        if (UIShow01.activeSelf)
        {
            UIShow02.SetActive(true);
         
        }
        else
        {
            UIShow01.SetActive(true);
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

        gameObject.SetActive(false); // Deactivate this game object (picked up item)
    }
}
