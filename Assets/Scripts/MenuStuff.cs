using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//This script was created for testing purposes only and will not be used in the final product.
public class MenuStuff : MonoBehaviour
{
    public string nextSceneName; // Name of the next scene to load
    public void B_LoadScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }


    public void B_QuitGame()
    {
        Application.Quit();
    }
}
