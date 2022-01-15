using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu, optionsMenu;

    public GameObject pauseFirstButton, optionsFirstButton, optionsCloseButon;
    public static bool optionsIsOpen;


    public void OpenOptions()
    {

        optionsIsOpen = true;
        optionsMenu.SetActive(true);
        pauseMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }

    public void CloseOptions()
    {
        optionsIsOpen = false;
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsCloseButon);
    }


    public void MainMenu()
    {
        AudioListener.pause = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        PlayerInfo.totalPoints = 0;
        PlayerInfo.hasBossKey = false;
        PlayerInfo.hasKey = false;
        PlayerInfo.hasShotgun = false;

    }

    public void RestartLevel()
    {
        AudioListener.pause = false;
        Time.timeScale = 1f;
        PauseManager.paused = false;
        PlayerInfo.hasBossKey = false;
        PlayerInfo.hasKey = false;
        PlayerInfo.totalPoints = 0;
        PlayerInfo.hasShotgun = false;
        PlayerPrefs.SetInt("PlayerCurrentLives", PlayerInfo.startingLives);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
