using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        MainMenu,
        Chapter01,
        LoadingScene,
    }

    private static Scene targetScene; // The scene we want to load after the loading scene

    // Public method to initiate scene loading from anywhere (e.g. Main Menu)
    public static void Load(Scene sceneToLoad)
    {
        // Set the target scene (e.g., Chapter01 or any other scene)
        targetScene = sceneToLoad;

        // Load the LoadingScene first
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    // This is called by the LoadingScene to load the target scene asynchronously
    public static IEnumerator LoadTargetSceneAsync()
    {
        // Ensure that targetScene is set
        if (targetScene == Scene.LoadingScene)
        {
            Debug.LogError("Target scene not set. Ensure Load() was called properly.");
            yield break;  // Avoid errors if the targetScene isn't set correctly
        }

        Debug.Log("Starting to load target scene: " + targetScene);
        
        // Start loading the target scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetScene.ToString());

        // Wait until the scene is fully loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
