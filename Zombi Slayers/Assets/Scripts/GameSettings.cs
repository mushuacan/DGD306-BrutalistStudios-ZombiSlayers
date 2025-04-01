using UnityEngine;
using System.Collections.Generic;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance; // Singleton pattern

    public Dictionary<string, object> settings = new Dictionary<string, object>();


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
        settings.Add("difficulty", "Medium");

    }
}