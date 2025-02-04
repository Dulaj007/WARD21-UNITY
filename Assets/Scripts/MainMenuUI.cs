using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Video;



public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button startButton;  // New Game Button
    [SerializeField] private Button exitButton;   // Exit Game Button
    [SerializeField] private Button continueButton; // Continue Game Button
    [SerializeField] private GameObject NowLoading; // Continue Game Button
    [SerializeField] private Button settingsBtn;
    [SerializeField] private Button BugReportBtn;
    [SerializeField] private Button CreditBtn;
    [SerializeField] private Button SkipBtn;
    [SerializeField] private GameObject settingsUI;
    [SerializeField] private AudioSource Click;
    [SerializeField] private VideoPlayer videoPlayer; 
    [SerializeField] private GameObject logo; 
    [SerializeField] private GameObject Buttons; 
    [SerializeField] private GameObject Keys; 

    [SerializeField] private AudioSource BackgroundSound;
    public static bool NewGame =false;

     private bool isVieoPlaying = false; 



    private void Start()
    {
       

        // Assign functionality to the Start (New Game) button
        if (startButton != null)
        {
           
            startButton.onClick.AddListener(OnStartButtonClicked);
        }

        // Assign functionality to the Exit button
        if (exitButton != null)
        {
             
            exitButton.onClick.AddListener(OnExitButtonClicked);
        }

        // Assign functionality to the Continue button
        if (continueButton != null)
        {
        
            continueButton.onClick.AddListener(OnContinueButtonClicked);
        }
         if (NowLoading != null)
        {
            NowLoading.SetActive(false); // Hide the pick-up text initially
     
        }
         if (settingsBtn != null)
        {
    
            settingsBtn.onClick.AddListener(SettingsBtn);
        }
          // Assign functionality to the Bug Report button
        if (BugReportBtn != null)
        {
            BugReportBtn.onClick.AddListener(OpenBugReportURL);
        }
         if (CreditBtn != null)
        {
            CreditBtn.onClick.AddListener(OpenCreditsURL);
        }

       
         if (videoPlayer != null)
        {
            videoPlayer.gameObject.SetActive(false); // Hide video player initially
        }
        if (videoPlayer == null)
        {
            Debug.Log("Video not asigned");
        }
    

    }
        void Update()
    {
        if (isVieoPlaying && Input.GetKeyDown(KeyCode.F))
        {
            SkipBtnClick();
            
        }
        
    }

   private void OnStartButtonClicked()
    {
        NewGame = true;
        Click.Play();
        if (videoPlayer != null)
        {
            StartCoroutine(PlayIntroVideo());
            
        }
        else
        {
            StartNewGame();
            Debug.Log("Starting new game without playing video..");
        }
    }

  private IEnumerator PlayIntroVideo()
{
    logo.SetActive(false);
    Buttons.SetActive(false);
    BackgroundSound.Stop();
   
    NowLoading.SetActive(true);
     Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    videoPlayer.gameObject.SetActive(true); // Show the video player
    videoPlayer.Play();

    // Wait until the video starts playing
    while (!videoPlayer.isPlaying)
    {
        Debug.Log("Waiting for the video to start...");
        yield return null;
    }
   
    NowLoading.SetActive(false);
    StartCoroutine(WaitForSubtitlesAndEnableSkip(10f));
    
    Debug.Log("Video started playing...");

    // Wait until the video finishes playing
    while (videoPlayer.isPlaying)
    {
        yield return null;
    }

    Debug.Log("Video finished playing...");

    videoPlayer.gameObject.SetActive(false); // Hide the video player

    StartNewGame(); // Proceed to the next function
}

    private void StartNewGame()
    {
        // Load the new game scene
        Loader.Load(Loader.Scene.Chapter01);
        NowLoading.SetActive(true);
         Keys.SetActive(true);
        Debug.Log("Starting a new game...");
    }

    private void OnExitButtonClicked()
    {
         Click.Play();
        // Exit the application
        Application.Quit();
        Debug.Log("Exiting the game...");
    }

    private void OnContinueButtonClicked()
    {
        NewGame = false;
         Click.Play();
        NowLoading.SetActive(true);
          Loader.Load(Loader.Scene.Chapter01);
        Debug.Log("Loading last save...");
     
   

    }
     public void SettingsBtn(){
         Click.Play();
        if (settingsBtn != null){
                settingsUI.SetActive(true);

        }

    }

    public void SkipBtnClick(){
        NewGame = true;
        Click.Play();
        videoPlayer.gameObject.SetActive(false); 
        SkipBtn.gameObject.SetActive(false);
        videoPlayer.Stop();
        StartNewGame();


    }
       private IEnumerator WaitForSubtitlesAndEnableSkip(float waitTime)
    {
        yield return new WaitForSeconds(waitTime); // Wait for the specified time (5 seconds)

        SkipBtn.gameObject.SetActive(true);
        isVieoPlaying = true;
    }

        private void OpenBugReportURL()
    {
        Click.Play();
        string bugReportURL = "https://your-bug-report-url.com";
        Application.OpenURL(bugReportURL);
        Debug.Log("Opening Bug Report URL...");
    }
         private void OpenCreditsURL()
    {
        Click.Play();
        string bugReportURL = "https://your-bug-report-url.com";
        Application.OpenURL(bugReportURL);
        Debug.Log("Opening credits Report URL...");
    }


}


