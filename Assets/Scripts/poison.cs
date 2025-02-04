using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class poison : MonoBehaviour
{

    public bool inReach;
     public GameObject pickUpText; // Text for displaying "Press X to pick up"
    public AudioSource pickUpSound; // Sound to play on picking up the note
    public GameObject GoodEnding;
    public GoodEnding end;
    public AudioSource backgroundSound;
   
    // Start is called before the first frame update
    void Start()
    {
   
        
    }

    // Update is called once per frame
    void Update()
    {
        //Check if the player presses the interact button and is within reach (E key)
        if (Input.GetKeyDown(KeyCode.E) && inReach)
        {

            Drink();
        }
        
    }
    
    void OnTriggerEnter(Collider other)
    {
        // Check if the player is within reach to interact
        if (other.CompareTag("Reach"))
        {
            inReach = true;
            pickUpText.SetActive(true); // Show the pick-up text
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the player has left the interaction range
        if (other.CompareTag("Reach"))
        {
            inReach = false;
            pickUpText.SetActive(false); // Hide the pick-up text
        }
    }
    public void Drink(){

    pickUpText.SetActive(false);  
    pickUpSound.Play();
    end.goodEnd();
    backgroundSound.Stop();


    // Activate the Death UI
    if (GoodEnding != null)
    {
        GoodEnding.SetActive(true);
    }
    }
    
  
}
