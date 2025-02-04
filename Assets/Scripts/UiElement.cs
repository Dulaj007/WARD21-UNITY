using UnityEngine;

public class UiElement : MonoBehaviour
{
    // Drag-and-drop reference to the GameObject to monitor
    [SerializeField] private GameObject targetGameObject;

    // Keeps track of the last known state of the target GameObject
    private bool lastTargetState;

    void Start()
    {
        // Initialize the state to match the target GameObject
        if (targetGameObject != null)
        {
            lastTargetState = targetGameObject.activeSelf;
            gameObject.SetActive(lastTargetState);
        }
    }

    void Update()
    {
        // Check if the target GameObject reference exists
        if (targetGameObject != null)
        {
            // Check if the active state of the target GameObject has changed
            if (targetGameObject.activeSelf != lastTargetState)
            {
                // Update the state of this GameObject to match the target
                lastTargetState = targetGameObject.activeSelf;
                gameObject.SetActive(lastTargetState);
            }
        }
    }
}
