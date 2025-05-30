using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public GameObject ButtonText;
    public GameObject ElevatorErro;
    public GameObject GreenLightOne;
    public GameObject GreenLightTwo;
    public OneLineSubs OneLineSubs;

    public GameObject Badend;
    public GoodEnding end;
    public AudioSource fix;
    public AudioSource backgroundSound;
    private bool inReach;

    void Start()
    {
        if (ElevatorErro != null)
        {
            ElevatorErro.SetActive(false);
        }


    }


    void Update()
    {
        if (inReach && Input.GetKeyDown(KeyCode.E))
        {
            CallElevator();
        }


    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = true;
            if (ButtonText != null)
            {
                ButtonText.SetActive(true);
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
                ButtonText.SetActive(false);
            }
            if (ElevatorErro != null)
            {
                ElevatorErro.SetActive(false);
            }
        }
    }
    void CallElevator()
    {
        if (GreenLightOne.activeSelf & GreenLightTwo.activeSelf)
        {
            ButtonText.SetActive(false);
            fix.Play();
            Badend.SetActive(true);
            end.goodEnd();
            backgroundSound.Stop();
        }
        else
        {
            if (ElevatorErro != null)
            {
                ElevatorErro.SetActive(true);
                if (OneLineSubs != null)
                {
                    OneLineSubs.StartOneLineSub();
                }

            }

        }


    }
}
