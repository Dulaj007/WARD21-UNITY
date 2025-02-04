using UnityEngine;
using UnityEngine.Playables; // For controlling Timeline

public class CutsceneManager : MonoBehaviour
{
    public PlayableDirector cutscene;  // Reference to the PlayableDirector
    public GameObject player;          // Reference to the player object or PlayerController script
    public MonoBehaviour cameraController; // Reference to the camera controller script (if any)
    public SubtitleManager subtitleManager; // Reference to SubtitleManager
    public SubtitleManager subtitleManager02; // Reference to SubtitleManager
    private PlayerController playerController; // Script controlling the player's movement

    public GameObject TutorialUI;

   

    void Start()
    {
        // Ensure PlayableDirector is assigned
        if (cutscene == null)
        {
            Debug.LogError("Cutscene PlayableDirector is not assigned!");
            return;
        }

      

        
        
        
        cutscene.stopped += OnCutsceneEnd; // Re-enable controls when cutscene finishes
    }

    private void OnCutsceneEnd(PlayableDirector director)
    {
        // Re-enable player and camera controls
        if (playerController != null)
        {
            playerController.enabled = true;
        }
        if (cameraController != null)
        {
            cameraController.enabled = true;
        }
         if (subtitleManager02 != null)
        {
            subtitleManager02.StartSubtitles();
        }
        TutorialUI.SetActive(true);
    }

    void OnDestroy()
    {
        cutscene.stopped -= OnCutsceneEnd; // Unregister event to prevent memory leaks
    }
    public void playCutscene(){
        cutscene.Play();
          // Get the player controller reference
        playerController = player.GetComponent<PlayerController>();

        // Disable player and camera control
        if (playerController != null)
        {
            playerController.enabled = false;
        }
        if (cameraController != null)
        {
            cameraController.enabled = false;
        }

        // Start the cutscene
        
        
        // Start subtitles if SubtitleManager is assigned
        if (subtitleManager != null)
        {
            subtitleManager.StartSubtitles();
        }
    }
}
