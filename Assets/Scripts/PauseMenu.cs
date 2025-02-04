using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class PauseMenu : MonoBehaviour
{
     public static PauseMenu Instance { get; private set; }
    public static bool isPaused = false; // Tracks whether the game is paused
    public GameObject pauseMenuUI;       // UI panel for the pause menu
    public GameObject player;            // Reference to the player GameObject
    public Pistol pistolScript;          // Reference to the Pistol script
    public Button resumeButton;          // Reference to the Resume button
    public Button ExitButton; 
    public Button reportBugButton;
    public Button settingsBtn;
    public GameObject settingsUI;
    public GameSettings GameSettings;
    public AudioSource Click;

    



 void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    void Start()
    {
        if (pistolScript == null)
        {
            pistolScript = player.GetComponent<Pistol>();
        }

        // Link the button if set
        if (resumeButton != null)
        {
            resumeButton.onClick.AddListener(Resume);
        }
         if (ExitButton != null)
        {
            ExitButton.onClick.AddListener(ExitToMainMenu);
        }
         if (reportBugButton != null)
        {
            reportBugButton.onClick.AddListener(ReportBug);
        }
          if (settingsBtn != null)
        {
            settingsBtn.onClick.AddListener(SettingsBtn);
        }
        


        Resume(); // Ensure the game starts in a resumed state
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
             Click.Play();
        pauseMenuUI.SetActive(false);
       if (GameSettings.soundOnStat == false)
            {
                AudioListener.pause = true; 
                // Your code here
            }
        if (GameSettings.soundOnStat == true)
            {
                AudioListener.pause = false; 
                // Your code here
            }
        Time.timeScale = 1f;
       
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (pistolScript != null)
        {
            pistolScript.canShoot = true;
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
             Click.Play();
        Time.timeScale = 0f;
        
         if (GameSettings.soundOnStat == true)
            {
                AudioListener.pause = true; 
                // Your code here
            }
    
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (pistolScript != null)
        {
            pistolScript.canShoot = false;
        }
    }

    public void LoadMenu()
    {
             Click.Play();
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
             Click.Play();
        Application.Quit();
        
    }
       private void ExitToMainMenu()
    {     Click.Play();
        // Assuming "MainMenu" is the name of your main menu scene
        Time.timeScale = 1f; // Ensure the game is unpaused before changing scenes
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
       private void ReportBug()
    {     Click.Play();
        // Opens the bug reporting URL in the browser
        Application.OpenURL("https://www.google.com");
    }
    public void SettingsBtn(){
             Click.Play();
        if (settingsBtn != null){
                settingsUI.SetActive(true);
        }

    }
}
