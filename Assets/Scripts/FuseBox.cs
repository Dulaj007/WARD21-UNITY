using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBox : MonoBehaviour
{
    public GameObject ButtonText;
    public GameObject RedLight;
    public GameObject GreenLight;
         public OneLineSubs OneLineSubs;
       

    private bool inReach; 
    // Start is called before the first frame update
    void Start()
    {
         if (RedLight != null)
        {
            RedLight.SetActive(true);
           
        }
        if (GreenLight != null)
        {
            GreenLight.SetActive(false);

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inReach && Input.GetKeyDown(KeyCode.E))
        {
            TurnOnFuse();
        }
        
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = true;
            if (ButtonText != null)
            {
                ButtonText.SetActive(true); // Show the pick-up text
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = false;
            if (ButtonText != null)
            {
                ButtonText.SetActive(false); // Hide the pick-up text
            }
        }
    }
    void TurnOnFuse()
    {
        if (RedLight != null)
        {
            RedLight.SetActive(false);
           
        }
        if (GreenLight != null)
        {
            GreenLight.SetActive(true);

        }
            if (OneLineSubs != null)
        {
            OneLineSubs.StartOneLineSub();
        }

    }
}
