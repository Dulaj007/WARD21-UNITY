
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Lock : MonoBehaviour
{
    public Animator phoneAnimator;
    public string correctCode = "1234";
    public TMP_InputField inputField;
    public GameObject interactText;
    public Button submitButton;
    public Button cancelButton;
    public GameObject player;



    public GameObject pauseMenuUI;




    private bool isNearPhone = false;
    private bool isPaused = false;
    private bool textHide = false;



    private void Start()
    {
        // Initially, hide the input field and buttons
        inputField.gameObject.SetActive(false);

        if (submitButton != null)
        {
            submitButton.gameObject.SetActive(false);
            submitButton.onClick.AddListener(OnSubmitCode);
        }


        if (cancelButton == null)
        {
            cancelButton.onClick.AddListener(cancelCode);
        }

        // Lock cursor 
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void Update()
    {
        // Check if player is near the phone and interaction is allowed
        if (isNearPhone && !inputField.gameObject.activeSelf && interactText.activeSelf && !isPaused)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ShowInputBox();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detect when the player enters the interaction range
        if (other.CompareTag("Reach"))
        {
            isNearPhone = true;

            if (!textHide)
            {
                interactText.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {  // Detect when the player leaves the interaction range
        if (other.CompareTag("Reach"))
        {
            isNearPhone = false;
            interactText.SetActive(false);
            inputField.gameObject.SetActive(false);

            if (submitButton != null)
            {
                submitButton.gameObject.SetActive(false);
            }

            ResumeGame();// Resume game when player moves away
        }
    }

    private void ShowInputBox()
    { // Show input field and submit button when interacting
        inputField.gameObject.SetActive(true);

        if (submitButton != null)
        {
            submitButton.gameObject.SetActive(true);
        }

        PauseGame(); // Pause game while entering code
    }

    private void OnSubmitCode()
    {
        string enteredCode = inputField.text;// Get the entered code



        if (enteredCode == correctCode)
        {
            // If the code is correct, play success animation
            phoneAnimator.SetBool("Right", true);
            phoneAnimator.SetBool("Wrong", false);



            Invoke(nameof(ResetRightBool), 3f);// Reset animation after 3 seconds
        }
        else
        {
            // If the code is wrong, play failure animation
            phoneAnimator.SetBool("Wrong", true);
            phoneAnimator.SetBool("Right", false);

            Invoke(nameof(ResetWrongBool), 3f);// Reset animation after 3 seconds
        }

        HideInteractText();// Hide interaction text
        Invoke(nameof(ShowInteractText), 10f);// Show interaction text again after 10 seconds
        // Hide UI elements after submitting
        inputField.gameObject.SetActive(false);

        if (submitButton != null)
        {
            submitButton.gameObject.SetActive(false);
        }
        cancelButton.gameObject.SetActive(false);

        ResumeGame();// Resume game after entering the code
    }

    private void ResetRightBool()
    {
        phoneAnimator.SetBool("Right", false);// Reset success animation
    }

    private void ResetWrongBool()
    {
        phoneAnimator.SetBool("Wrong", false);// Reset failure animation
    }

    private void HideInteractText()
    {
        interactText.SetActive(false);
        textHide = true;// Mark text as hidden
    }

    private void ShowInteractText()
    {
        if (isNearPhone && textHide)
        {
            interactText.SetActive(true);
            textHide = false;// Allow interaction text to reappear
        }
    }

    private void PauseGame()
    {// Pause the game and show pause menu
        interactText.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;// Stop time in the game
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;


    }

    private void ResumeGame()
    {// Resume the game and hide pause menu
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;// Resume time
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;


    }
    public void cancelCode()
    {

        // Cancel code entry and hide UI elements
        inputField.gameObject.SetActive(false);
        submitButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        ResumeGame();// Resume gameplay

    }
}
