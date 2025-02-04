using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    private Light lightSource;
    public float minIntensity = 0.5f; // Minimum light intensity
    public float maxIntensity = 2.0f; // Maximum light intensity
    public float flickerSpeed = 0.1f; // How quickly the light flickers

    void Start()
    {
        lightSource = GetComponent<Light>();
        if (lightSource == null)
        {
            Debug.LogWarning("No Light component found on this object.");
        }
    }

    void Update()
    {
        if (lightSource != null)
        {
            // Set a random intensity within the min and max range
            lightSource.intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.PerlinNoise(Time.time * flickerSpeed, 0.0f));
        }
    }
}
