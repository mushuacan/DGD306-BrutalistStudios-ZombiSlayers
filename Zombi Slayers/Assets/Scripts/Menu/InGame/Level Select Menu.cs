using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectMenu : MonoBehaviour
{
    [SerializeField] private Button[] levels;
    public int currentLevel;

    private void Start()
    {
        currentLevel = (int)GameSettings.Instance.settings["level"] + 1;

        for (int i = 0; i < currentLevel; i++)
        {
            levels[i].interactable = true;
        }
        for (int i = currentLevel; i < levels.Length; i++)
        {
            levels[i].interactable = false;
        }


        EventSystem.current.SetSelectedGameObject(levels[0].gameObject);
    }

    public void Button_LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
