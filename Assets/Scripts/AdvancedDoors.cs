﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedDoors : MonoBehaviour
{

    public Animator door;
    public GameObject lockOB;
    public GameObject keyOB;
    public GameObject keyUI;
    public GameObject openText;
    public GameObject closeText;
    public GameObject lockedText;

    public AudioSource openSound;
    public AudioSource closeSound;
    public AudioSource lockedSound;
    public AudioSource unlockedSound;

    private bool inReach;
    private bool doorisOpen;
    private bool doorisClosed;
    public bool locked;
    public bool unlocked;

    public KeyCode toggleKey = KeyCode.E;



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach" && doorisClosed)
        {
            inReach = true;
            openText.SetActive(true);
        }

        if (other.gameObject.tag == "Reach" && doorisOpen)
        {
            inReach = true;
            closeText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            openText.SetActive(false);
            lockedText.SetActive(false);
            closeText.SetActive(false);
        }
    }

    void Start()
    {
        inReach = false;
        doorisClosed = true;
        doorisOpen = false;
        closeText.SetActive(false);
        openText.SetActive(false);
    }


    void Update()
    {
        if (lockOB.activeInHierarchy)
        {
            locked = true;
            unlocked = false;
        }

        else
        {
            unlocked = true;
            locked = false;
        }

        if (inReach && keyOB.activeInHierarchy && Input.GetKeyDown(toggleKey))
        {
            unlockedSound.Play();
            locked = false;
            keyOB.SetActive(false);
            keyUI.SetActive(false);
            StartCoroutine(unlockDoor());
        }

        if (inReach && doorisClosed && unlocked && Input.GetKeyDown(toggleKey))
        {
            door.SetBool("Open", true);
            door.SetBool("Closed", false);
            openText.SetActive(false);
            openSound.Play();
            doorisOpen = true;
            doorisClosed = false;
        }

        else if (inReach && doorisOpen && unlocked && Input.GetKeyDown(toggleKey))
        {
            door.SetBool("Open", false);
            door.SetBool("Closed", true);
            closeText.SetActive(false);
            closeSound.Play();
            doorisClosed = true;
            doorisOpen = false;
        }

        if (inReach && locked && Input.GetKeyDown(toggleKey))
        {
            openText.SetActive(false);
            lockedText.SetActive(true);
            lockedSound.Play();
        }

    }

    IEnumerator unlockDoor()
    {
        yield return new WaitForSeconds(.05f);
        {

            unlocked = true;
            lockOB.SetActive(false);
        }
    }




}
