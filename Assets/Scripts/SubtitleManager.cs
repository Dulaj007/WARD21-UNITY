using UnityEngine;
using TMPro; // For TextMeshPro
using System.Collections;

public class SubtitleManager : MonoBehaviour
{
    public TMP_Text subtitleText;  // Reference to the subtitle Text UI
    public string[] subtitles;     // Array of subtitle lines
    public float[] timings;        // Timings for each subtitle (in seconds)
    public float fadeDuration = 0.5f; // Duration for the fade-in effect
    public float pauseBetweenLines = 0.3f; // Pause duration after each line before clearing
    public AudioSource Voice;

    private Coroutine subtitleCoroutine;

    public void StartSubtitles()
    {
        if (subtitleCoroutine != null)
        {
            StopCoroutine(subtitleCoroutine);
        }
          if (Voice != null)
        {
             Voice.Play();
        }
       

        subtitleCoroutine = StartCoroutine(PlaySubtitles());
    }

    private IEnumerator PlaySubtitles()
    {
       
        for (int i = 0; i < subtitles.Length; i++)
        {
            yield return StartCoroutine(FadeInSubtitle(subtitles[i]));
            yield return new WaitForSeconds(timings[i]);

            yield return new WaitForSeconds(pauseBetweenLines); // Pause after each line
        }

        subtitleText.text = ""; // Clear subtitle at the end
        
    }

    private IEnumerator FadeInSubtitle(string subtitle)
    {
        subtitleText.text = subtitle;
        subtitleText.alpha = 0; // Start fully transparent

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            subtitleText.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration); // Smoothly increase opacity
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        subtitleText.alpha = 1; // Ensure it's fully visible at the end
    }
}
