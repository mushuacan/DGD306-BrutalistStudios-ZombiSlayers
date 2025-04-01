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
    public GameObject fBO_MainMenu;
    public GameObject fBO_Credits;
    public GameObject fBO_Settings;

    public Scrollbar scrollbar_Sounds;
    public Scrollbar scrollbar_SoundFXs;
    public Scrollbar scrollbar_Music; 
    
    public Toggle toggleComponent;


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

    public void Scrollbar_Sounds(float value)
    {
        GameSettings.Instance.settings["mainSoundsVolume"] = value;
    }
    public void Scrollbar_SoundFXs(float value)
    {
        GameSettings.Instance.settings["soundFXVolume"] = value;
    }
    public void Scrollbar_Musics(float value)
    {
        GameSettings.Instance.settings["musicVolume"] = value;
    }
    public void ArrangeScrollbarsAblity()
    {
        bool areSoundsOn = (bool)GameSettings.Instance.settings["areVolumesOn"];
        if (areSoundsOn)
        {
            scrollbar_Sounds.interactable = true;
            scrollbar_SoundFXs.interactable = true;
            scrollbar_Music.interactable = true;
        }
        else
        {
            scrollbar_Sounds.interactable = false;
            scrollbar_SoundFXs.interactable = false;
            scrollbar_Music.interactable = false;
        }
    }

    public void ArrangeSoundsToggle(bool isOn)
    {
        GameSettings.Instance.settings["areVolumesOn"] = isOn;

        ArrangeScrollbarsAblity();
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
