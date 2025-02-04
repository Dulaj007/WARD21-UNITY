using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objectsToActivate; // Array to hold the GameObjects to be activated

    // This method is called when the GameObject becomes active
    private void OnEnable()
    {
        // Check if the GameObject this script is attached to is active
        if (gameObject.activeSelf)
        {
            // Loop through the array and activate each GameObject
            foreach (GameObject obj in objectsToActivate)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                }
            }
        }
    }
}
