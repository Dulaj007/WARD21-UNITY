using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Include TextMeshPro namespace

public class GoodEnding : MonoBehaviour
{
    public GameObject goodEndUi;
    public GameObject credits;
    public AudioSource EndingSong;
    public Button Continue;
    public AudioSource Click;
    public OneLineSubs OneLineSubs;
    public GameObject player;
    public Pistol pistolScript;
    public PlayerController playerController;
    public GameObject healArm;

    // Start is called before the first frame update
    void Start()
    {
        if (Continue != null)
        {
            Continue.onClick.AddListener(ContinueCredits);
            Continue.gameObject.SetActive(false); // Ensure Continue button is hidden initially
        }
    }

    public void goodEnd()
    {
        EndingSong.Play();
        Debug.Log("song played");
       
        if (OneLineSubs != null)
        {
            OneLineSubs.StartOneLineSub(); // Start the subtitles
        }

        // Start the coroutine to wait for 5 seconds after the subtitles finish
        //StartCoroutine(WaitForSubtitlesAndEnableContinue(5f)); // Adjust the time if needed
    }
    private void OnEnable()
    {
        StartCoroutine(WaitForSubtitlesAndEnableContinue(30f));

         if (pistolScript == null && player != null)
        {
            pistolScript = player.GetComponent<Pistol>();
        }
        if (playerController == null && player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }
        // Deactivate both scripts to prevent player movement and gun usage during subtitles
if (pistolScript != null)
{
    pistolScript.enabled = false;  // Disable the pistol script (prevents shooting and other related actions)
}

if (playerController != null)
{
    playerController.enabled = false;;  // Disable the player movement script (prevents movement)
}
healArm.SetActive(false);
    }


    private IEnumerator WaitForSubtitlesAndEnableContinue(float waitTime)
    {
        yield return new WaitForSeconds(waitTime); // Wait for the specified time (5 seconds)

        // Now, activate the Continue button
        if (Continue != null)
        {
            Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
            Continue.gameObject.SetActive(true); // Show the Continue button after waiting
        }
    }

    public void ContinueCredits()
    {
        if (Click != null)
        {
            Click.Play(); // Play the click sound
        }

        Debug.Log("Continue clicked");

        credits.SetActive(true);
        goodEndUi.SetActive(false);

        // Handle credit screen transitions (as per your original code)
    }
}
