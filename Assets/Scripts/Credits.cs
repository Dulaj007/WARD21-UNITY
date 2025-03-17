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

    void Start()
    {
        if (ExitButton != null)
        {
            ExitButton.onClick.AddListener(ExitToMainMenu);
        }
    }


    void Update()
    {


    }
    private void OnEnable()
    {
        CreditAnimator.SetBool("scroll", true);

    }
    private void ExitToMainMenu()
    {
        Click.Play();

        Time.timeScale = 1f; // Ensure the game is unpaused before changing scenes
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
