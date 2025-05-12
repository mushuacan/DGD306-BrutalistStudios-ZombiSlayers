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
        if (clip == null || clip.Length == 0)
        {
            Debug.LogWarning("Clip boþ ya da null.");
            return;
        }

        int index = Random.Range(0, clip.Length);
        audioSource.PlayOneShot(clip[index]);
    }

}
