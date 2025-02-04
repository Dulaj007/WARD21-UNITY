using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneLineSubs : MonoBehaviour


{
    public SubtitleManager subtitleManager; // Reference to SubtitleManager
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartOneLineSub (){
          if (subtitleManager != null)
        {
            Debug.Log("One Line Sub triggered");
            subtitleManager.StartSubtitles();
        }
    }
}
