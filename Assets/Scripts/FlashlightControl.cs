using UnityEngine;

public class FlashlightControl : MonoBehaviour
{
    public Light flashlight; // Drag and drop the flashlight Light component here

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
