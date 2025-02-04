using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoMove : MonoBehaviour
{
    public Animator Photo;
     public AudioSource KeySound; 
    private bool inReach = false; 
     private BoxCollider triggerCollider;
    public  KeyCode toggleKey = KeyCode.E;
    public GameObject FixText;
    // Start is called before the first frame update
    void Start()
    {
        FixText.SetActive(false);
            // Cache the reference to the Box Collider on this GameObject
        triggerCollider = GetComponent<BoxCollider>();

        if (triggerCollider == null)
        {
            Debug.LogWarning("Box Collider not found on " + gameObject.name);
        }
        
    }

      void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = true;

            FixText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = false;
            FixText.SetActive(false);
        }
    }

    void Update()
    {
        if (inReach && Input.GetKeyDown(toggleKey))
        {
           MovePhoto();
        }
    }
    void MovePhoto()
    {
     
        Photo.SetBool("Move", true);
        Debug.Log("Play key sound");
        KeySound.Play();
        FixText.SetActive(false);
        inReach = false;
          // Disable the trigger Box Collider to prevent re-triggering
            if (triggerCollider != null)
            {
                triggerCollider.enabled = false;
                Debug.Log("Box Collider disabled after triggering.");
            }
    
    }

}
