using System.Collections;
using UnityEngine;

public class ZombieSouns : MonoBehaviour
{
    public AudioSource Screm;
    public float fadeOutDuration = 1.0f; // Time in seconds for fade-out

    private Coroutine fadeOutCoroutine;

    // Method to start playing screams
    public void PlayScrem()
    {
        if (!Screm.isPlaying)
        {
            Screm.volume = 1.0f; // Ensure volume is reset before playing
            Screm.Play();
        }
    }

    // Method to stop screams smoothly
    public void StopScrem()
    {
        if (fadeOutCoroutine != null)
        {
            StopCoroutine(fadeOutCoroutine);
        }

        fadeOutCoroutine = StartCoroutine(FadeOutSound());
    }

    // Coroutine to fade out the sound
    private IEnumerator FadeOutSound()
    {
        float startVolume = Screm.volume;

        while (Screm.volume > 0)
        {
            Screm.volume -= startVolume * Time.deltaTime / fadeOutDuration;
            yield return null;
        }

        Screm.Stop();
        Screm.volume = startVolume; // Reset volume for the next playback
    }
}
