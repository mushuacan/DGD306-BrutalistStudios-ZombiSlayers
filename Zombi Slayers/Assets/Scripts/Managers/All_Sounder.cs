using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class All_Sounder : MonoBehaviour
{
    public static All_Sounder Instance; // Singleton pattern

    public AudioSource audioSource;

    // Her anahtar (key) i�in en son �al�nma zaman�
    private Dictionary<string, float> lastPlayedTimes = new Dictionary<string, float>();

    // Debounce s�resi (ayn� sesin tekrar �al�nmadan �nceki minimum s�re)
    public float debounceTime = 0.3f;


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


    /// <summary>
    /// Verilen ses listesinden rastgele bir tanesini �alar. �stenirse ayn� sesi k�sa s�rede tekrar �almay� �nler.
    /// </summary>
    /// <param name="clips">Ses klip dizisi</param>
    /// <param name="clipKey">Bu ses grubuna �zel anahtar</param>
    /// <param name="preventReplay">true ise ayn� sesi k�sa s�rede yeniden �almay� engeller</param>
    public void ChooseAndPlaySoundOf(AudioClip[] clips, string clipKey = "Unspecified", bool preventReplay = true)
    {
        if (clips == null || clips.Length == 0)
        {
            Debug.LogWarning("Clip bo� ya da null.");
            return;
        }

        // E�er �nleme aktifse ve bu key'e ait ses yak�n zamanda �al�nd�ysa, �alma
        if (preventReplay && lastPlayedTimes.TryGetValue(clipKey, out float lastTime))
        {
            if (Time.time - lastTime < debounceTime)
                return;
        }

        // Rastgele bir ses se� ve �al
        int index = Random.Range(0, clips.Length);
        AudioClip selectedClip = clips[index];

        audioSource.PlayOneShot(selectedClip);

        // E�er �nleme aktifse, bu key i�in son �alma zaman�n� g�ncelle
        if (preventReplay)
        {
            lastPlayedTimes[clipKey] = Time.time;
        }
    }
}
