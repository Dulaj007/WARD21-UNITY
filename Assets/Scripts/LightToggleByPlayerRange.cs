using UnityEngine;

public class LightToggleByPlayerRange : MonoBehaviour
{
    private Light lightSource; // Reference to the Light component
    public Transform player; // Reference to the player object
    public float detectionRange = 10f; // Public variable to adjust the detection range

    void Start()
    {
        // Get the Light component on this object
        lightSource = GetComponent<Light>();
        if (lightSource == null)
        {
            Debug.LogWarning("No Light component found on this object. Disabling script.");
            this.enabled = false;
        }

        // Ensure the player Transform is assigned
        if (player == null)
        {
            Debug.LogError("Player reference not assigned. Please assign the player Transform.");
            this.enabled = false;
        }
    }

    void Update()
    {
        // Check if the player is within the detection range
        if (Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            if (!lightSource.enabled) // Turn on the light if it’s off
                lightSource.enabled = true;
        }
        else
        {
            if (lightSource.enabled) // Turn off the light if it’s on
                lightSource.enabled = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw the detection range in the Scene view for debugging
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
