using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class All_Sounder : MonoBehaviour
{
    public static All_Sounder Instance; // Singleton pattern

    public AudioSource audioSource;


    private void Awake()
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
    }

    public void ChooseAndPlaySoundOf(AudioClip[] clip)
    {
        if (clip == null) Debug.LogWarning("Clip'te hata var");
        audioSource.PlayOneShot(clip[Random.Range(0, clip.Length)]);
    }
}
