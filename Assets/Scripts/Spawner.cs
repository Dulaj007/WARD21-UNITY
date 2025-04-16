using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> zombies = new List<GameObject>();
    public float detectionRange = 20.0f;
    public Transform player;
    private int dieCount = 0;

    private bool zombiesActivated = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Step 1: Activate zombies if player is within detection range
        if (!zombiesActivated && distanceToPlayer <= detectionRange)
        {
            for (int i = 0; i < zombies.Count; i++)
            {
                if (zombies[i] != null)
                {
                    zombies[i].SetActive(true);
                }
            }
            zombiesActivated = true;
        }

        // Step 2: If zombies were activated, check if all of them are destroyed
        if (zombiesActivated)
        {
            bool allZombiesDestroyed = true;

            for (int i = 0; i < zombies.Count; i++)
            {
                if (zombies[i] != null)
                {
                    allZombiesDestroyed = false;
                    break;
                }
            }

            if (allZombiesDestroyed)
            {
                // Deactivate the spawner
                gameObject.SetActive(false);
            }
        }
    }
}