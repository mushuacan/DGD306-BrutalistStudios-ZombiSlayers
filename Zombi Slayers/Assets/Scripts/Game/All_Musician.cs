using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class All_Musician : MonoBehaviour
{
    public static All_Musician Instance; // Singleton pattern

    public AudioSource audioSource;

    public AudioClip[] menuClip;
    public AudioClip[] gameClip;
    public AudioClip[] bossClip;

    public float fadeOutDuration = 1f;
    public float fadeInDuration = 0.3f;
    public string lastClipName;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public void ChooseAndPlaySoundOf(AudioClip[] clips)
    {
        if (clips == null || clips.Length == 0)
        {
            Debug.LogWarning("Clip boþ ya da null.");
            return;
        }

        int index = Random.Range(0, clips.Length);
        AudioClip selectedClip = clips[index];

        // Fade out aktif sesi
        DOTween.To(() => audioSource.volume, x => audioSource.volume = x, 0f, fadeOutDuration)
        .OnComplete(() =>
        {
            audioSource.Stop();
            audioSource.clip = selectedClip;
            audioSource.Play();
            DOTween.To(() => audioSource.volume, x => audioSource.volume = x, 1f, fadeInDuration);
        });

    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Menu Scene" || scene.name == "Player Selection Scene" )
        {
            if (lastClipName != "Menu Scene")
            {
                lastClipName = "Menu Scene";
                ChooseAndPlaySoundOf(menuClip);
            }
        }
        else if (scene.name == "Level_1" || scene.name == "Level_2" || scene.name == "Level_3" || scene.name == "Level_4" || scene.name == "Level_5" || 
            scene.name == "Level_6" || scene.name == "Level_7" || scene.name == "Level_8" || scene.name == "Level_9" || scene.name == "Test Level 3")
        {
            lastClipName = "Game";
            ChooseAndPlaySoundOf(gameClip);
        }
        else if (scene.name == "Level_10")
        {
            lastClipName = "Game";
            ChooseAndPlaySoundOf(bossClip);
        }
    }
}
