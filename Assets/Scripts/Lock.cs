
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
        //interactText.SetActive(true);
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
            isNearPhone = true;

            if (!textHide)
            {
                interactText.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            isNearPhone = false;
            interactText.SetActive(false);
            inputField.gameObject.SetActive(false);

            if (submitButton != null)
            {
                submitButton.gameObject.SetActive(false);
            }

            ResumeGame();
        }
    }

    private void ShowInputBox()
    {
        inputField.gameObject.SetActive(true);

        if (submitButton != null)
        {
            submitButton.gameObject.SetActive(true);
        }

        PauseGame();
    }

    private void OnSubmitCode()
    {
        string enteredCode = inputField.text;
         
       

        if (enteredCode == correctCode)
        {
              
            phoneAnimator.SetBool("Right", true);
            phoneAnimator.SetBool("Wrong", false);

      

            Invoke(nameof(ResetRightBool), 3f);
        }
        else
        {
      
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
        cancelButton.gameObject.SetActive(false);

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

    
    }

    private void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;


    }
    public void cancelCode(){

       
        inputField.gameObject.SetActive(false);
        submitButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        ResumeGame();

    }
}
