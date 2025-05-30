using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//This script was created for testing purposes only and will not be used in the final product.
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject player;
    public Button loadButton;
    public Button saveButton;

    private string savePath;
    public GameData LoadedGameData;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        savePath = Application.persistentDataPath + "/gameSave.json"; //Game save file path C:\Users\Username\AppData\LocalLow\CompanyName\GameName

        if (loadButton != null)
            loadButton.onClick.AddListener(LoadGame);

        if (saveButton != null)
            saveButton.onClick.AddListener(SaveGame);
        else
            Debug.LogWarning("Save button is not assigned in the Inspector!");
    }

    public void SaveGame()
    {
        if (LoadedGameData == null)
        {
            LoadedGameData = new GameData();
        }

        if (player != null)
        {
            LoadedGameData.playerPosition = player.transform.position;
        }
        else
        {
            Debug.LogWarning("Player reference is not set in the GameManager!");
        }

        File.WriteAllText(savePath, JsonUtility.ToJson(LoadedGameData));
        Debug.Log("Game saved successfully!");
    }

    public void LoadGame()
    {

        PauseMenu.Instance.Resume();

        if (!File.Exists(savePath))
        {
            Debug.LogWarning("No save file found at " + savePath);
            return;
        }

        LoadedGameData = JsonUtility.FromJson<GameData>(File.ReadAllText(savePath));
        if (LoadedGameData != null)
        {
            Debug.Log("Game data loaded. Restarting the scene...");

            // Reload the current scene
            ApplyLoadedGameData();
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Debug.LogError("Failed to load game data!");
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (LoadedGameData != null)
        {
            ApplyLoadedGameData();
        }
    }

    private void ApplyLoadedGameData()
    {
        if (LoadedGameData == null)
        {
            Debug.LogError("No loaded game data to apply!");
            return;
        }

        if (player != null)
        {
            player.transform.position = LoadedGameData.playerPosition;
            Debug.Log("Player position restored: " + player.transform.position);
        }
        else
        {
            Debug.LogWarning("Player reference is not set in the GameManager!");
        }


        // Restore doors
        var doorObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Door"));
        for (int i = 0; i < doorObjects.Count && i < LoadedGameData.saveitems.Count; i++)
        {
            if (doorObjects[i] != null)
            {
                doorObjects[i].SetActive(!LoadedGameData.saveitems[i].isLocked);
            }
        }

        Debug.Log("Game data successfully applied after scene reload.");
    }
}
