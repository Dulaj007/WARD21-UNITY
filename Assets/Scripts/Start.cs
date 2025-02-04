using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starting : MonoBehaviour
{
     public CutsceneManager Cutscene01;
     public SaveSystem savesystem;

    // Start is called before the first frame update
    void Start()
    {
           if (MainMenuUI.NewGame) // Access the static variable from the Menu script
        {
            Debug.Log("New Game is true");
     
            Cutscene01.playCutscene();
            // Perform functionality for New Game
        }
        else
        {
            Debug.Log("New Game is false");
            savesystem.LoadGame();
            // Perform functionality for other case
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   private void OnEnable(){
 

   }

    
}
