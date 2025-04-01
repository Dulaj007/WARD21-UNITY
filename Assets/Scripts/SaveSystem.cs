using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
public class SaveSystem : MonoBehaviour
{
    public GameObject player;

    public GameObject LoadingSaveScreen;
    public List<GameObject> zombies = new List<GameObject>();
    public List<GameObject> doors = new List<GameObject>();
    public GameObject saveDone;
    public GameObject saveFailed;

    public Button saveButton;
    public Button loadButton;
    public GameData LoadedGameData;
    public CutsceneManager Cutscene01;

    private string savePath;

    void Start()
    {
        savePath = Application.persistentDataPath + "/gameSave.json";
        Debug.Log("Loading game from: " + savePath);

        if (saveButton != null)
        {
            saveButton.onClick.AddListener(SaveGame);
        }
        if (loadButton != null)
        {
            loadButton.onClick.AddListener(loadbutn);
        }
        else
        {
            Debug.LogWarning("Save button is not assigned in the Inspector!");
        }

        if (MainMenuUI.NewGame)// Access the static variable from the Menu script
        {
            Debug.Log("New Game is true");

            Cutscene01.playCutscene(); //play the intro cutscene

        }


        else
        {
            LoadingSaveScreen.SetActive(true);
            Debug.Log("New Game is false");
            StartCoroutine(WaitTOLoad(10f));

        }
        SceneManager.sceneLoaded += OnSceneLoaded;



    }

    private IEnumerator WaitTOLoad(float waitTime)
    {
        yield return new WaitForSeconds(waitTime); // Wait for the specified time (5 seconds)
        Debug.Log("Waiting before game load");
        LoadGame();
        LoadingSaveScreen.SetActive(false);

    }
    public void loadbutn()
    {
        LoadingSaveScreen.SetActive(true);
        Debug.Log("New Game is false");
        MainMenuUI.NewGame = false; //calling a game restart with newgame variable false
        Loader.Load(Loader.Scene.Chapter01);

    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the loaded scene is Chapter01
        if (scene.name == Loader.Scene.Chapter01.ToString())
        {
            Debug.Log("Chapter01 has been loaded successfully!");

        }
    }

    public void SaveGame()
    {
        try
        {
            GameData data = new GameData();

            // Save player position
            data.playerPosition = player.transform.position;

            // Save zombie data - Only add alive zombies to the save data
            foreach (var zombie in zombies)
            {
                if (zombie != null && zombie.activeSelf) // Only save if zombie is still alive
                {
                    ZombieData zData = new ZombieData
                    {
                        position = zombie.transform.position,
                        isAlive = true // Save as alive if active
                    };
                    data.zombies.Add(zData);
                }
            }

            // Save door data
            // later this method used save some other gameobject as well
            foreach (var door in doors)
            {
                DoorData dData = new DoorData
                {
                    isLocked = door.activeSelf //save according to active state of the object
                };
                data.doors.Add(dData);
            }

            // Write to JSON file
            File.WriteAllText(savePath, JsonUtility.ToJson(data));

            Debug.Log("Game saved to " + savePath);

            // If successful, show success message
            saveDone.SetActive(true);
            saveFailed.SetActive(false);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to save game: " + e.Message);

            // If failed, show failure message
            saveFailed.SetActive(true);
            saveDone.SetActive(false);
        }
    }

    // this method not used
    public string GetSavePath()
    {
        return savePath;
    }

    public void LoadGame()
    {
        savePath = Application.persistentDataPath + "/gameSave.json"; //get save path
        Debug.Log("LoadGame method called");
        Debug.Log("Loading game from: " + savePath);
        if (!File.Exists(savePath))
        {
            Debug.LogWarning("No save file found at " + savePath);
            RestartGame();
            return;
        }
        Debug.Log("Save file found, loading data...");

        // Load saved game data
        LoadedGameData = JsonUtility.FromJson<GameData>(File.ReadAllText(savePath));
        PauseMenu.Instance.Resume();




        // Restore player position
        Debug.Log("Loading player position: " + LoadedGameData.playerPosition);
        if (player == null)
        {
            Debug.LogError("Player object not found!");
            return;
        }

        var movementScript = player.GetComponent<PlayerController>();

        if (movementScript != null)
        {
            movementScript.enabled = false; // Temporarily disable movement
        }

        if (player.TryGetComponent(out Rigidbody rb))
        {
            rb.MovePosition(LoadedGameData.playerPosition); // Use Rigidbody to set position
        }
        else
        {
            player.transform.position = LoadedGameData.playerPosition; // Standard transform update
        }

        if (movementScript != null)
        {
            movementScript.enabled = true; // Re-enable movement
        }

        Debug.Log("Player position after loading: " + player.transform.position);

        // Restore zombie data
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Only restore zombies that exist in the save data
        for (int i = 0; i < LoadedGameData.zombies.Count; i++)
        {
            ZombieData zData = LoadedGameData.zombies[i];

            // Check if zombie exists in the scene and restore position only if alive
            GameObject zombie = enemies.FirstOrDefault(e => e.transform.position == zData.position);
            if (zombie != null)
            {
                zombie.SetActive(zData.isAlive); // Only restore alive zombies
                if (!zData.isAlive)
                {
                    Destroy(zombie);
                }


                if (zombie.TryGetComponent(out Rigidbody zombieRb))
                {
                    zombieRb.MovePosition(zData.position);
                }
                else
                {
                    zombie.transform.position = zData.position;
                }
            }
            else
            {
                // If zombie does not exist anymore (killed and not in the scene), skip restoring it
                Debug.Log("Zombie at " + zData.position + " does not exist anymore.");
            }
        }

        Debug.Log("Zombies restored: " + LoadedGameData.zombies.Count);


        // Restore door data 
        // Doors are not find by using a tag its simply set active all the selected gameobjects
        int doorCount = Mathf.Min(doors.Count, LoadedGameData.doors.Count); // Handle mismatched counts

        for (int i = 0; i < doorCount; i++)
        {
            GameObject door = doors[i];
            DoorData dData = LoadedGameData.doors[i];

            // Set door lock state (locked or unlocked)

            // Here, just enable or disable the door based on the saved state
            // door.SetActive(dData.isLocked);  (this is the simple way to store door data)
            if (door != null)
            {
                if (dData.isLocked == true)
                {
                    door.SetActive(true); // Activate the door (locked)
                }
                else
                {
                    door.SetActive(false); // Deactivate the door (unlocked)
                }
                // If locked (isLocked == true), set inactive (locked); else, active (unlocked)
                Debug.Log(dData.isLocked);
                Debug.Log(dData);
            }
        }

        Debug.Log("Doors restored: " + doorCount);


    }
    private void RestartGame()
    {
        // Logic to restart the game from the beginning
        Debug.Log("Restarting game from the beginning...");

        // Example: Load the initial game scene
        MainMenuUI.NewGame = true;
        Loader.Load(Loader.Scene.Chapter01);



    }


}

[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public List<ZombieData> zombies = new List<ZombieData>();
    public List<DoorData> doors = new List<DoorData>();
}

[System.Serializable]
public class ZombieData
{
    public Vector3 position;
    public bool isAlive;
}

[System.Serializable]
public class DoorData
{
    public bool isLocked;
}
