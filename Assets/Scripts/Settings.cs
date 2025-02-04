using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    public AudioSource backgroundSound;
    public Button BackgroundSoundOn;
    public Button BackgroundSoundOf;
    public Button SoundOnn;
    public Button SoundOff;
    public Button Back;
    public GameObject settings;
    public bool soundOnStat;
    public AudioSource Click;

    // Start is called before the first frame update
    void Start()
    {

        soundOnStat= true;
        BackgroundSoundOn.gameObject.SetActive(false);

         if (BackgroundSoundOn != null)
        {
            BackgroundSoundOn.onClick.AddListener(backgroundSoundOn);
        }
         if (BackgroundSoundOf != null)
        {
            BackgroundSoundOf.onClick.AddListener(backgroundSoundOf);
        }
          if (Back != null)
        {
            Back.onClick.AddListener(Backf);
        }
         if (SoundOff != null)
        {
            SoundOff.onClick.AddListener(SoundOf);
        }
         if (SoundOnn != null)
        {
            SoundOnn.onClick.AddListener(SoundOn);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void backgroundSoundOn(){
          Click.Play();
        backgroundSound.Play();
        BackgroundSoundOn.gameObject.SetActive(false);
        BackgroundSoundOf.gameObject.SetActive(true);

    }
     public void backgroundSoundOf(){
          Click.Play();
        backgroundSound.Stop();
        BackgroundSoundOf.gameObject.SetActive(false);
        BackgroundSoundOn.gameObject.SetActive(true);

    }
    public void Backf(){
          Click.Play();
        settings.SetActive(false);
    }
    public void SoundOf(){
          Click.Play();
          AudioListener.pause = true; 
          soundOnStat=false;
        SoundOff.gameObject.SetActive(false);
        SoundOnn.gameObject.SetActive(true);

    }
    public void SoundOn(){
          Click.Play();
        AudioListener.pause = false; 
        soundOnStat=true;
        SoundOnn.gameObject.SetActive(false);
        SoundOff.gameObject.SetActive(true);
        

    }
}
