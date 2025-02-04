using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PhoneInteraction : MonoBehaviour
{
    public GameObject thisPhone; // Reference to this phone object
    public Animator phoneAnimator;
    public string correctCode = "";
    public TMP_InputField inputField;
    public GameObject interactText;
    public Button submitButton;
    public Button cancelButton;
    public GameObject player;

    public AudioSource Ring;
    public AudioSource Wrong;

    public GameObject pauseMenuUI;
    public Pistol pistolScript;

    private bool isNearPhone = false;
    private bool isPaused = false;
    private bool textHide = false;
    private static GameObject activePhone;

    private void Start()
    {
        // Ensure thisPhone is assigned
        if (thisPhone == null)
        {
            thisPhone = gameObject;
        }

        if (activePhone != null && activePhone != thisPhone)
            return;

      
        activePhone = null;

        // Hide input field and buttons initially
        inputField.gameObject.SetActive(false);

        if (submitButton != null)
        {
            submitButton.gameObject.SetActive(false);
            submitButton.onClick.AddListener(OnSubmitCode);
        }

        if (cancelButton != null)
        {
            cancelButton.gameObject.SetActive(false);
            cancelButton.onClick.AddListener(cancelCode);
        }

        if (pistolScript == null && player != null)
        {
            pistolScript = player.GetComponent<Pistol>();
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
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
    if (other.CompareTag("Reach"))
    {
        if (activePhone == null)
        {
            isNearPhone = true;

            if (!textHide)
            {
                interactText.SetActive(true);
            }

            activePhone = thisPhone; // Set the active phone
            Debug.Log("Active phone set: " + activePhone.name);
        }
        else
        {
            Debug.LogWarning($"Attempt to set active phone but another phone is already active: {activePhone.name}");
        }
    }
}

private void OnTriggerExit(Collider other)
{
    if (other.CompareTag("Reach") && activePhone == thisPhone)
    {
        isNearPhone = false;
        interactText.SetActive(false);
        activePhone = null; // Clear only if this phone is the active phone
        inputField.gameObject.SetActive(false);

        if (submitButton != null)
        {
            submitButton.gameObject.SetActive(false);
        }

        ResumeGame();
        Debug.Log("Cleared active phone: " + thisPhone.name); // Debug message
    }
}


    private void ShowInputBox()
    {
        inputField.gameObject.SetActive(true);

        if (submitButton != null)
        {
            submitButton.gameObject.SetActive(true);
        }

        if (cancelButton != null)
        {
            cancelButton.gameObject.SetActive(true);
        }

        PauseGame();
    }

    private void OnSubmitCode()
    {
        Debug.Log("Active phone set: " + activePhone?.name);
        Debug.Log("Current phone: " + thisPhone.name);
        Debug.Log("Is active phone correct: " + (activePhone == thisPhone));
        
        if (activePhone != thisPhone)
            return;

        string enteredCode = inputField.text;

        if (enteredCode == correctCode)
        {
            if (Ring != null)
            {
                Ring.Play();
            }

            phoneAnimator.SetBool("Right", true);
            phoneAnimator.SetBool("Wrong", false);
            Invoke(nameof(ResetRightBool), 3f);
        }
        else
        {
            if (Wrong != null)
            {
                Wrong.Play();
            }

            phoneAnimator.SetBool("Wrong", true);
            phoneAnimator.SetBool("Right", false);
            Invoke(nameof(ResetWrongBool), 3f);
        }

        HideInteractText();
        Invoke(nameof(ShowInteractText), 10f);

        inputField.gameObject.SetActive(false);

        if (submitButton != null)
        {
            submitButton.gameObject.SetActive(false);
        }

        if (cancelButton != null)
        {
            cancelButton.gameObject.SetActive(false);
        }

        ResumeGame();
    }

    private void ResetRightBool()
    {
        phoneAnimator.SetBool("Right", false);
    }

    private void ResetWrongBool()
    {
        phoneAnimator.SetBool("Wrong", false);
    }

    private void HideInteractText()
    {
        interactText.SetActive(false);
        textHide = true;
    }

    private void ShowInteractText()
    {
        if (isNearPhone && textHide)
        {
            interactText.SetActive(true);
            textHide = false;
        }
    }

    private void PauseGame()
    {
        interactText.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;

        if (pistolScript != null)
        {
            pistolScript.canShoot = false;
        }
    }

    private void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;

        if (pistolScript != null)
        {
            pistolScript.canShoot = true;
        }
    }

    public void cancelCode()
    {
        inputField.gameObject.SetActive(false);

        if (submitButton != null)
        {
            submitButton.gameObject.SetActive(false);
        }

        if (cancelButton != null)
        {
            cancelButton.gameObject.SetActive(false);
        }

        ResumeGame();
    }
}
