using UnityEngine;
using System.Collections.Generic;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance; // Singleton pattern

    public Dictionary<string, object> settings = new Dictionary<string, object>();

    [SerializeField] private AudioMixerManager mixerManager;

    [Header("Changes")]
    [SerializeField] private bool changeSettingsAsDown;
    [SerializeField] private int startLevel = 0;
    [SerializeField] private bool initialAnimations;
    [SerializeField] private bool Volumes;
    [SerializeField] private float MasterVolume;


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
        settings.Add("level", 0);
        settings.Add("gizem", false);

        if (changeSettingsAsDown)
        {
            settings["animations"] = initialAnimations;
            settings["areVolumesOn"] = Volumes;
            settings["mainSoundsVolume"] = MasterVolume;
            settings["level"] = startLevel;
        }
    }
    private void Start()
    {
        ApplySettings();
    }

    public void ApplySettings()
    {
        Debug.Log("Sesler düzenlendi.");
        mixerManager.ApplySettingsToMixer();
    }

    public void ApplySounds()
    {
        mixerManager.SetMasterVolume();
    }
}