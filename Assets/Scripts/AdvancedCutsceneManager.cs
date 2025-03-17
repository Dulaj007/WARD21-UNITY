using UnityEngine;
using UnityEngine.Playables;

public class AdvancedCutsceneManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector cutscene;
    [SerializeField] private GameObject player;
    [SerializeField] private MonoBehaviour cameraController;
    [SerializeField] private TriggerCutscene triggerCutscene;

    public AdvancedSubtitleManager subtitleManager;

    private PlayerController playerController;

    void Start()
    {
        if (cutscene == null)
        {
            Debug.LogError("Cutscene PlayableDirector is not assigned!");
            return;
        }

        if (player == null)
        {
            Debug.LogError("Player object is not assigned!");
            return;
        }

        if (triggerCutscene != null)
        {
            triggerCutscene.OnTriggerCutscene += PlayCutscene;
        }

        playerController = player.GetComponent<PlayerController>();

        if (playerController == null)
        {
            Debug.LogError("PlayerController component is missing on the player object!");
        }


    }

    public void PlayCutscene()
    {
        if (cutscene.state == PlayState.Playing)
        {
            Debug.LogWarning("Cutscene is already playing!");
            return;
        }

        if (playerController != null)
        {
            playerController.canMove = false;
        }

        if (cameraController != null)
        {
            cameraController.enabled = false;
        }


        cutscene.time = 0;
        cutscene.Play();

        if (subtitleManager != null)
        {
            subtitleManager.StartSubtitles();
        }

        cutscene.stopped += OnCutsceneEnd;
    }

    private void OnCutsceneEnd(PlayableDirector director)
    {
        if (playerController != null)
        {
            playerController.canMove = true;
        }


        if (cameraController != null)
        {
            cameraController.enabled = true;
        }

        if (subtitleManager != null)
        {
            subtitleManager.StopSubtitles();
        }

        cutscene.stopped -= OnCutsceneEnd;
    }

    void OnDestroy()
    {
        if (triggerCutscene != null)
        {
            triggerCutscene.OnTriggerCutscene -= PlayCutscene;
        }

        if (cutscene != null)
        {
            cutscene.stopped -= OnCutsceneEnd;
        }
    }
}
