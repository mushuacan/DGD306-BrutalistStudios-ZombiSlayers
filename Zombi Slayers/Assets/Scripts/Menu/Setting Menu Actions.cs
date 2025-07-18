using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingMenuActions : MonoBehaviour
{
    [Header("Settings")]
    public TextMeshProUGUI difficultyText;
    public TextMeshProUGUI warningerText;
    public Scrollbar scrollbar_Warningers;
    public Scrollbar scrollbar_Difficulty;
    public Scrollbar scrollbar_Sounds;
    public Scrollbar scrollbar_SoundFXs;
    public Scrollbar scrollbar_Music;
    public Toggle toggleComponentSound;
    public Toggle toggleComponentAnimation;
    public bool areTogglesOn;

    public bool showExitButton = true;
    public GameObject exitButton;
    // Start is called before the first frame update
    void Start()
    {
        toggleComponentSound.isOn = (bool)GameSettings.Instance.settings["areVolumesOn"];
        toggleComponentAnimation.isOn = (bool)GameSettings.Instance.settings["animations"];
    }

    private void OnEnable()
    {
        SetValuesOfSettings();
    }

    #region SETTINGS

    public void SetValuesOfSettings()
    {
        scrollbar_Difficulty.value = (float)GameSettings.Instance.settings["difficulty"];
        scrollbar_Warningers.value = (float)GameSettings.Instance.settings["warningers"];
        scrollbar_Music.value = (float)GameSettings.Instance.settings["musicVolume"];
        scrollbar_SoundFXs.value = (float)GameSettings.Instance.settings["soundFXVolume"];
        scrollbar_Sounds.value = (float)GameSettings.Instance.settings["mainSoundsVolume"];
        toggleComponentSound.isOn = (bool)GameSettings.Instance.settings["areVolumesOn"];
        toggleComponentAnimation.isOn = (bool)GameSettings.Instance.settings["animations"];
        exitButton.SetActive(showExitButton);
        EventSystem.current.SetSelectedGameObject(scrollbar_Difficulty.gameObject);
        ArrangeDifficultyText();
        ArrangeWarningersText();
    }

    public void Toggle_Sounds()
    {
        GameSettings.Instance.settings["areVolumesOn"] = toggleComponentSound.isOn;
        Debug.Log("Toggle: " + toggleComponentSound.isOn + " | areVolumesOn: " + GameSettings.Instance.settings["areVolumesOn"]);

        ArrangeScrollbarsAblity();
    }

    public void Toggle_AnimationSetting()
    {
        GameSettings.Instance.settings["animations"] = toggleComponentAnimation.isOn;
        Debug.Log("Toggle: " + toggleComponentAnimation.isOn + " | animations: " + GameSettings.Instance.settings["animations"]);

        ArrangeScrollbarsAblity();
    }
    public void Scrollbar_Difficulty()
    {
        GameSettings.Instance.settings["difficulty"] = scrollbar_Difficulty.value;
        ArrangeDifficultyText();
    }
    public void Scrollbar_Warningers()
    {
        GameSettings.Instance.settings["warningers"] = scrollbar_Warningers.value;
        ArrangeWarningersText();
    }
    public void Scrollbar_Sounds()
    {
        GameSettings.Instance.settings["mainSoundsVolume"] = scrollbar_Sounds.value;
        GameSettings.Instance.ApplySettingsToMixer();
    }
    public void Scrollbar_SoundFXs()
    {
        GameSettings.Instance.settings["soundFXVolume"] = scrollbar_SoundFXs.value;
        GameSettings.Instance.ApplySettingsToMixer();
    }
    public void Scrollbar_Musics()
    {
        GameSettings.Instance.settings["musicVolume"] = scrollbar_Music.value;
        GameSettings.Instance.ApplySettingsToMixer();
    }
    public void Button_exit()
    {
        PlayerManager pm = FindAnyObjectByType<PlayerManager>();
        pm.KillAllPlayers();
    }
    public void ArrangeScrollbarsAblity()
    {
        bool areSoundsOn = (bool)GameSettings.Instance.settings["areVolumesOn"];
        GameSettings.Instance.ApplySounds();
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
    public void ArrangeDifficultyText()
    {
        float difficulty = (float)GameSettings.Instance.settings["difficulty"];
        if (difficulty == 0)
        {
            difficultyText.text = "Difficulty: Easy";
        }
        else if (difficulty == 0.5f)
        {
            difficultyText.text = "Difficulty: Medium";
        }
        else if (difficulty == 1)
        {
            difficultyText.text = "Difficulty: Hard";
        }
        else
        {
            difficultyText.text = "Difficulty: ?";
        }
    }
    public void ArrangeWarningersText()
    {
        float warningers = (float)GameSettings.Instance.settings["warningers"];
        if (warningers == 0)
        {
            warningerText.text = "Warner: Nothing";
        }
        else if (warningers == 0.25)
        {
            warningerText.text = "Warner: Bullets";
        }
        else if (warningers == 0.5f)
        {
            warningerText.text = "Warner: Bullets & Zombi's";
        }
        else if (warningers == 0.75f)
        {
            warningerText.text = "Warner: Bullets & Supplies";
        }
        else if (warningers == 1)
        {
            warningerText.text = "Warner: All";
        }
        else
        {
            warningerText.text = "Warner: ?";
        }
    }
    #endregion
}
