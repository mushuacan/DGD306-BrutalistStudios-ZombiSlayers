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

        if (levels == null || levels.Length == 0)
        {
            Debug.LogWarning("Level listesi boþ veya null.");
            return;
        }

        int safeLevel = Mathf.Clamp(currentLevel, 0, levels.Length); // Aþmamasý garanti

        for (int i = 0; i < safeLevel; i++)
        {
            levels[i].interactable = true;
        }
        for (int i = safeLevel; i < levels.Length; i++)
        {
            levels[i].interactable = false;
        }


        int index = currentLevel - 1;

        if (index >= 0 && index < levels.Length)
        {
            EventSystem.current.SetSelectedGameObject(levels[index].gameObject);
        }
        else if (levels.Length > 0)
        {
            // Eðer geçerli index yoksa, ilk elementi seç
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
