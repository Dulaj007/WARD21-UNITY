using UnityEngine;
using UnityEngine.UI;

public class DeathUI : MonoBehaviour
{
    public Button LoadButton; // Button to load the game
    public Button ExitButton; // Button to exit to the main menu
    public SaveSystem SaveSystem; // Reference to the save system
    public PlayerHealth playerHealth;
    public GameObject deathUi;

    private void Start()
    {
        if (LoadButton != null)
        {
            LoadButton.onClick.AddListener(LoadGame);
        }

        if (ExitButton != null)
        {
            ExitButton.onClick.AddListener(ExitToMainMenu);
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        AudioListener.pause = true; 
    }

    private void LoadGame()
    {
        if (SaveSystem != null)
        {
            
            SaveSystem.LoadGame();
              // Restore health to maximum and reset UI effects
      playerHealth.RestoreHealth(100); 
        playerHealth.UpdateBloodEffectsWhenHeal();
        playerHealth.isDead = false;

        // Disable the Death UI and unpause the game
        deathUi.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;

       

        }
        else
        {
            Debug.LogError("SaveSystem is not assigned in the inspector!");
        }
    }

    private void ExitToMainMenu()
    {
        // Assuming "MainMenu" is the name of your main menu scene
        Time.timeScale = 1f; // Ensure the game is unpaused before changing scenes
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
