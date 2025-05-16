using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtonActions : MonoBehaviour
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
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);

        //EventSystem.current.SetSelectedGameObject(fBO_Settings.gameObject);
    }

    public void Button_LoadScene(string scene)
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


    #region CREDITS
    public void Button_Open_URL(string URL)
    {
        Application.OpenURL(URL);
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
