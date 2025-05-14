using UnityEngine;
using System.Collections.Generic;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance; // Singleton pattern

    public Dictionary<string, object> settings = new Dictionary<string, object>();

    [SerializeField] private AudioMixerManager mixerManager;
    [SerializeField] private int startLevel = 0;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object between scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }

        // Initialize default settings
        settings.Add("soundFXVolume", 1.0f);
        settings.Add("musicVolume", 1.0f);
        settings.Add("mainSoundsVolume", 1.0f);
        settings.Add("areVolumesOn", true);
        settings.Add("animations", true);
        settings.Add("difficulty", "Medium");
        settings.Add("level", startLevel);
    }

    public void ApplySettings()
    {
        mixerManager.ApplySettingsToMixer();
    }

    public void ApplySounds()
    {
        mixerManager.SetMasterVolume();
    }
}