using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    public GameObject CreditUI;
    public Animator CreditAnimator;
    public Button ExitButton;
    public AudioSource Click;
    // Start is called before the first frame update
    void Start()
    {
        if (ExitButton != null)
        {
            ExitButton.onClick.AddListener(ExitToMainMenu);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
    private void OnEnable()
    {
        CreditAnimator.SetBool("scroll", true);

    }
        private void ExitToMainMenu()
    {     Click.Play();
        // Assuming "MainMenu" is the name of your main menu scene
        Time.timeScale = 1f; // Ensure the game is unpaused before changing scenes
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
