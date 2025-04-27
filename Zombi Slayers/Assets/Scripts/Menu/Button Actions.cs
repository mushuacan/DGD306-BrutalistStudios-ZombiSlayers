using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonActions : MonoBehaviour
{
    #region Variables
    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject creditsMenu;

    [Header("fBO's")]
    // fBO = firstButtonOf...
    public GameObject fBO_MainMenu;
    public GameObject fBO_Credits;
    public GameObject fBO_Settings;

    [Header("Credits")]
    // fBO = firstButtonOf...
    public GameObject mushu;
    public GameObject emrei;
    public GameObject hay;
    public GameObject everyone;

    [Header("Settings")]
    public Scrollbar scrollbar_Sounds;
    public Scrollbar scrollbar_SoundFXs;
    public Scrollbar scrollbar_Music; 
    public Toggle toggleComponent;

    [Header("Other")]
    public bool startFromMainMenu;

    #endregion

    private void Start()
    {
        if (!startFromMainMenu) return;

        #region Menu
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        #endregion

        #region Credits
        mushu.SetActive(false);
        emrei.SetActive(false);
        hay.SetActive(false);
        #endregion

        #region Settings
        toggleComponent.isOn = (bool)GameSettings.Instance.settings["areVolumesOn"];
        #endregion

        EventSystem.current.SetSelectedGameObject(fBO_MainMenu.gameObject);
    }


    #region MAIN MENU
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
        SetValuesOfSettings();

        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(fBO_Settings.gameObject);
    }

    public void Button_LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Buttons_Exit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    #endregion

    #region SETTINGS

    public void SetValuesOfSettings()
    {
        scrollbar_Music.value = (float)GameSettings.Instance.settings["musicVolume"];
        scrollbar_SoundFXs.value = (float)GameSettings.Instance.settings["soundFXVolume"];
        scrollbar_Sounds.value = (float)GameSettings.Instance.settings["mainSoundsVolume"];
        toggleComponent.isOn = (bool)GameSettings.Instance.settings["areVolumesOn"];
    }

    public void Toggle_Sounds()
    {
        GameSettings.Instance.settings["areVolumesOn"] = toggleComponent.isOn;
        Debug.Log("Toggle: " + toggleComponent.isOn + " | areVolumesOn: " + GameSettings.Instance.settings["areVolumesOn"]);

        ArrangeScrollbarsAblity();
    }

    public void Scrollbar_Sounds()
    {
        GameSettings.Instance.settings["mainSoundsVolume"] = scrollbar_Sounds.value;
    }
    public void Scrollbar_SoundFXs()
    {
        GameSettings.Instance.settings["soundFXVolume"] = scrollbar_SoundFXs.value;
    }
    public void Scrollbar_Musics()
    {
        GameSettings.Instance.settings["musicVolume"] = scrollbar_Music.value;
    }
    public void ArrangeScrollbarsAblity()
    {
        bool areSoundsOn = (bool) GameSettings.Instance.settings["areVolumesOn"];
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
    #endregion

    #region CREDITS
    public void Button_URL_mushuacan()
    {
        // Burada istediðin URL'yi giriyorsun
        string url = "https://mushuacan.itch.io/"; // Linki buraya yaz
        Application.OpenURL(url);
    }

    public void Button_Credits_Mushu()
    {
        mushu.SetActive(!mushu.activeSelf);
        CheckIfEveryoneActive();
    }
    public void Button_Credits_Emrei()
    {
        emrei.SetActive(!emrei.activeSelf);
        CheckIfEveryoneActive();
    }
    public void Button_Credits_Hay()
    {
        hay.SetActive(!hay.activeSelf);
        CheckIfEveryoneActive();
    }

    public void CheckIfEveryoneActive()
    {
        if (mushu.activeSelf && emrei.activeSelf && hay.activeSelf)
        {
            everyone.SetActive(true);
        }
        else
        {
            everyone.SetActive(false);
        }
    }

    #endregion
}
