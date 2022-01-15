using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour
{
    PauseAction action;
    public static bool paused;
    public GameObject menu;
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    

    private void Awake()
    {
        action = new PauseAction();
    }
    private void OnEnable()
    {
        action.Enable();
    }
    private void OnDisable()
    {
        action.Disable();
    }
    private void Start()
    {
        paused = false;
        action.Pause.PauseGame.performed += _ => DeterminePause();
        
       
    }

    private void DeterminePause()
    {
        if (paused)
        {
            ResumeGame();
        } else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        menu.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;
        paused = true;

    }

    public void ResumeGame()
    {

        if (PauseMenu.optionsIsOpen)
        {
            pauseMenu.SetActive(true);
            optionsMenu.SetActive(false);
            PauseMenu.optionsIsOpen = false;
        } else
        {
            menu.SetActive(false);
            paused = false;
            AudioListener.pause = false;
            Time.timeScale = 1f;
        }


        



    }
}
