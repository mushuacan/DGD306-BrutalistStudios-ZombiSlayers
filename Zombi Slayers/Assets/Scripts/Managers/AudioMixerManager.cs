using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerManager : MonoBehaviour
{
    public AudioMixer mixer;

    public void ApplySettingsToMixer()
    {
        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
    }
    public void SetMasterVolume()
    {
        if ((bool)GameSettings.Instance.settings["areVolumesOn"])
        {
            SetVolume((float)GameSettings.Instance.settings["mainSoundsVolume"], "MasterVolume");
        }
        else
        {
            SetVolume(0f, "MasterVolume");
        }
    }
    public void SetMusicVolume()
    {
        SetVolume((float)GameSettings.Instance.settings["musicVolume"], "MusicVolume");
    }
    public void SetSFXVolume()
    {
        SetVolume((float)GameSettings.Instance.settings["soundFXVolume"], "SFXVolume");
    }
    private void SetVolume(float volume, string exposedParam)
    {
        // Logaritmik dönüþüm (0 için -80 dB, 1 için 0 dB)
        float dB = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
        mixer.SetFloat(exposedParam, dB);
    }
}
