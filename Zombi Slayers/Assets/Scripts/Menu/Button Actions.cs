using DG.Tweening.Core.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonActions : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject creditsMenu;

    // fBO = firstButtonOf...
    public Button fBO_MainMenu;
    public Button fBO_Credits;
    public Button fBO_Settings;

    public bool startFromMainMenu;

    private void Start()
    {
        if (!startFromMainMenu) return;

        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);

        EventSystem.current.SetSelectedGameObject(fBO_MainMenu.gameObject);
    }

    public void Button_SetCurrent()
    {
        EventSystem.current.SetSelectedGameObject(fBO_MainMenu.gameObject);
    }

    public void Button_OpenCredits()
    {
        creditsMenu.SetActive(true);
        mainMenu.SetActive(false);

        EventSystem.current.SetSelectedGameObject(fBO_Credits.gameObject);
    }
    public void Button_OpenMainMenu()
    {
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        mainMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(fBO_MainMenu.gameObject);
    }

    public void Button_OpenSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(fBO_Settings.gameObject);
    }

    public void Button_LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Buttons_Exit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
