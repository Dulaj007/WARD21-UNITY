using UnityEngine;
using UnityEngine.UI;
using System;

public class TriggerCutscene : MonoBehaviour
{
    public GameObject interactText; // UI text for interaction prompt
    public event Action OnTriggerCutscene; // Event for triggering the cutscene

    private bool inReach; // Tracks if the player is near the trigger
    private bool cutscenePlayed = false; // Tracks if the cutscene has already been played

    void Start()
    {
        if (interactText != null)
        {
            interactText.SetActive(false); // Hide interaction text initially
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach") && !cutscenePlayed)
        {
            inReach = true;
            if (interactText != null)
            {
                interactText.SetActive(true); // Show interaction text
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = false;
            if (interactText != null)
            {
                interactText.SetActive(false); // Hide interaction text
            }
        }
    }

    void Update()
    {
        if (inReach && Input.GetKeyDown(KeyCode.E) && !cutscenePlayed)
        {
            cutscenePlayed = true; // Prevent re-triggering the cutscene
            interactText.SetActive(false); // Hide the interaction text
            OnTriggerCutscene?.Invoke(); // Trigger the cutscene
        }
    }
}
