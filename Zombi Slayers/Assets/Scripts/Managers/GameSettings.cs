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
        settings.Add("difficulty", 1f); // 0 easy, 0.5 medium, 1 hard
        settings.Add("warningers", 0.5f); // 0 0,25 0,5 0,75 1
        settings.Add("level", 0);
        settings.Add("gizem", false);

        if (changeSettingsAsDown)
        {
            settings["areVolumesOn"] = Volumes;
            settings["mainSoundsVolume"] = MasterVolume;
            settings["level"] = startLevel;
        }
    }
    private void Start()
    {
        ApplySettingsToMixer();
    }

    public void ApplySettingsToMixer()
    {
        Debug.Log("Sesler düzenlendi.");
        mixerManager.ApplySettingsToMixer();
    }

    public void ApplySounds()
    {
        mixerManager.SetMasterVolume();
    }
}