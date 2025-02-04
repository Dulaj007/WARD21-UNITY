using UnityEngine;


public class AmmoPickup : MonoBehaviour
{
    public GameObject pickUpText;          // UI text to show when the player is near the object
    public AudioSource pickUpSound;       // Sound to play when the object is picked up
    public int ammoAmount = 10;           // Amount of ammo to add to the pistol

    public bool inReach;                 // Flag to check if the player is within reach

    public Pistol playerPistol;          // Reference to the player's pistol script

    void Start()
    {
        if (pickUpText != null)
        {
            pickUpText.SetActive(false); // Hide the pick-up text initially
           
        }

        // Find the Pistol script attached to the player
    
    }

    void Update()
    {
        // Check for pick-up interaction
        if (inReach && Input.GetKeyDown(KeyCode.T))
        {
                playerPistol = FindObjectOfType<Pistol>();
        if (playerPistol == null)
        {
            Debug.Log("Pistol script not found in the scene.");
        }
             Debug.Log("Calling pick up ammo.");
            PickUpAmmo();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach")) // Ensure it's the player interacting
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
        if (other.CompareTag("Reach")) // Ensure it's the player leaving the interaction zone
        {
            inReach = false;
            if (pickUpText != null)
            {
                pickUpText.SetActive(false); // Hide the pick-up text
            }
        }
    }

    void PickUpAmmo()
    {
        if (playerPistol != null)
        {
            Debug.Log("Picking up ammo...");
            Debug.Log("Ammo before pickup: " + playerPistol.currentAmmoInStorage);

            // Add ammo to the pistol storage
            playerPistol.currentAmmoInStorage += ammoAmount;
            playerPistol.UpdateAmmoDisplay();

            Debug.Log("Ammo after pickup: " + playerPistol.currentAmmoInStorage);

            // Play the pick-up sound
            if (pickUpSound != null)
            {
                pickUpSound.Play();
            }
            if (pickUpText != null)
            {
                pickUpText.SetActive(false); // Hide the pick-up text
            }

            // Destroy the ammo object
            Destroy(gameObject);
        }
    }
}
