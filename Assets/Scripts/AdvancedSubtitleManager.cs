using UnityEngine;
using TMPro; // For TextMeshPro
using System.Collections;
using System.Collections.Generic;

public class AdvancedSubtitleManager : MonoBehaviour
{
    [Header("Subtitle UI Elements")]
    public TMP_Text subtitleText;   // Reference to the subtitle Text UI

    [Header("Subtitle Data")]
    public List<SubtitleData> subtitleEntries = new List<SubtitleData>(); // List of subtitles with timings

    [Header("Fade Settings")]
    public float fadeDuration = 0.5f;  // Duration for fade-in and fade-out effects
    public float pauseBetweenLines = 0.3f;  // Pause duration after each line before clearing

    private Coroutine subtitleCoroutine;

    [System.Serializable]
    public class SubtitleData
    {
        [TextArea]
        public string text;  // The actual subtitle text
        public float displayTime;  // Duration to display this subtitle
    }

    public void StartSubtitles()
    {
        if (subtitleCoroutine != null)
        {
            StopCoroutine(subtitleCoroutine);
        }

        subtitleCoroutine = StartCoroutine(PlaySubtitles());
    }

    public void StopSubtitles()
    {
        if (subtitleCoroutine != null)
        {
            StopCoroutine(subtitleCoroutine);
        }

        subtitleText.text = ""; // Clear subtitle
        subtitleText.alpha = 0; // Ensure it's fully transparent
    }

    private IEnumerator PlaySubtitles()
    {
        foreach (SubtitleData entry in subtitleEntries)
        {
            yield return StartCoroutine(FadeInSubtitle(entry.text));
            yield return new WaitForSeconds(entry.displayTime);

            yield return StartCoroutine(FadeOutSubtitle());
            yield return new WaitForSeconds(pauseBetweenLines);
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

    private IEnumerator FadeOutSubtitle()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            subtitleText.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration); // Smoothly decrease opacity
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        subtitleText.alpha = 0; // Ensure it's fully transparent at the end
        subtitleText.text = ""; // Clear text after fading out
    }
}
