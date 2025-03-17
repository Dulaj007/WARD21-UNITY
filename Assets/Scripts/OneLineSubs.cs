using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneLineSubs : MonoBehaviour


{
    public SubtitleManager subtitleManager; // Reference to SubtitleManager

    void Start()
    {


    }

    void Update()
    {

    }

    public void StartOneLineSub()
    {
        if (subtitleManager != null)
        {
            Debug.Log("One Line Sub triggered");
            subtitleManager.StartSubtitles();
        }
    }
}
