using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectMenu : MonoBehaviour
{
    [SerializeField] private Button[] levels;
    [SerializeField] private GameObject levelMenu;
    [SerializeField] private GameObject exitMenu;
    [SerializeField] private GameObject exitMenuFirstButton;
    public int currentLevel;
    public bool workWithCurrentLevel;
    public int totalLevels;

    private void Start()
    {
        if (workWithCurrentLevel)
        {
            currentLevel = (int)GameSettings.Instance.settings["level"] + 1;
        }
        else { currentLevel = totalLevels; }


        for (int i = 0; i < currentLevel; i++)
        {
            levels[i].interactable = true;
        }
        for (int i = currentLevel; i < levels.Length; i++)
        {
            levels[i].interactable = false;
        }

        EventSystem.current.SetSelectedGameObject(levels[currentLevel - 1].gameObject);

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(levels[0].gameObject);
        }
    }

    public void Button_OpenExitMenu()
    {
        levelMenu.SetActive(false);
        exitMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(exitMenuFirstButton.gameObject);
    }
    public void Button_CloseExitMenu()
    {
        levelMenu.SetActive(true);
        exitMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(levels[currentLevel - 1].gameObject);
    }
    public void Button_ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void Button_LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
