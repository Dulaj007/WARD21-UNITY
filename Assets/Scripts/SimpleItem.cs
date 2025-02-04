using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleItem : MonoBehaviour
{
    public Animator item; // Animator controlling the item's animations
    public GameObject openText; // "Press E to Open" UI text
    public GameObject CloseText; // "Press E to Close" UI text
    public  KeyCode toggleKey = KeyCode.C; // Key to toggle the item
    public AudioSource OpenSound;
    private bool inReach = false; // Flag to check if player is within range
    private bool itemIsOpen = false; // Tracks whether the item is open or closed


    void Start()
    {
        if (openText == null || CloseText == null || item == null)
        {
           
            enabled = false;
            return;
        }
         if (item == null)
        {
            Debug.LogError($"Animator is not assigned on GameObject: {gameObject.name}", this);
            enabled = false;
            return;
        }
        if (openText == null)
        {
            Debug.LogError($"OpenText is not assigned on GameObject: {gameObject.name}", this);
            enabled = false;
            return;
        }
        if (CloseText == null)
        {
            Debug.LogError($"CloseText is not assigned on GameObject: {gameObject.name}", this);
            enabled = false;
            return;
        }

        openText.SetActive(false);
        CloseText.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = true;

            if (!itemIsOpen)
            {
                openText.SetActive(true);  // Show "Press E to Open" text
                CloseText.SetActive(false);
            }
            else
            {
                CloseText.SetActive(true);  // Show "Press E to Close" text
                openText.SetActive(false);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = false;
            openText.SetActive(false);
            CloseText.SetActive(false);
        }
    }

    void Update()
    {
        if (inReach && Input.GetKeyDown(toggleKey))
        {
            if (!itemIsOpen)
                OpenItem();
            else
                CloseItem();
        }
    }

    void ResetAnimationBools()
    {
        item.SetBool("Open", false);
        item.SetBool("Close", false);
    }

    void OpenItem()
    {
        ResetAnimationBools();
        item.SetBool("Open", true);
        openText.SetActive(false);
        itemIsOpen = true;
         if (OpenSound != null)
            {
                OpenSound.Play();
            }
    }

    void CloseItem()
    {
        ResetAnimationBools();
        item.SetBool("Close", true);
        CloseText.SetActive(false);
        itemIsOpen = false;
    }
}
