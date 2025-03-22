using UnityEngine;

public class FlashlightControl : MonoBehaviour
{
    //This script was created for testing purposes only and will not be used in the final product.
    public Light flashlight;

    // Method to turn on the flashlight
    public void TurnOnFlashlight()
    {
        if (flashlight != null)
        {
            flashlight.enabled = true;
        }
        else
        {
            Debug.LogError("Flashlight component not assigned.");
        }
    }

    // Method to turn off the flashlight
    public void TurnOffFlashlight()
    {
        if (flashlight != null)
        {
            flashlight.enabled = false;
        }
        else
        {
            Debug.LogError("Flashlight component not assigned.");
        }
    }
}
