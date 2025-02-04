using System.Collections;
using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    private void Start()
    {
        // Start loading the target scene asynchronously once the LoadingScene is loaded
        StartCoroutine(Loader.LoadTargetSceneAsync());
    }
}
