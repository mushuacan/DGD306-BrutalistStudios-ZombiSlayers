using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class All_Sounder : MonoBehaviour
{
    public static All_Sounder Instance; // Singleton pattern

    public AudioSource audioSource;

    // Her anahtar (key) için en son çalýnma zamaný
    private Dictionary<string, float> lastPlayedTimes = new Dictionary<string, float>();

    // Debounce süresi (ayný sesin tekrar çalýnmadan önceki minimum süre)
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
    /// Verilen ses listesinden rastgele bir tanesini çalar. Ýstenirse ayný sesi kýsa sürede tekrar çalmayý önler.
    /// </summary>
    /// <param name="clips">Ses klip dizisi</param>
    /// <param name="clipKey">Bu ses grubuna özel anahtar</param>
    /// <param name="preventReplay">true ise ayný sesi kýsa sürede yeniden çalmayý engeller</param>
    public void ChooseAndPlaySoundOf(AudioClip[] clips, string clipKey = "Unspecified", bool preventReplay = true)
    {
        if (clips == null || clips.Length == 0)
        {
            Debug.LogWarning("Clip boþ ya da null.");
            return;
        }

        // Eðer önleme aktifse ve bu key'e ait ses yakýn zamanda çalýndýysa, çalma
        if (preventReplay && lastPlayedTimes.TryGetValue(clipKey, out float lastTime))
        {
            if (Time.time - lastTime < debounceTime)
                return;
        }

        // Rastgele bir ses seç ve çal
        int index = Random.Range(0, clips.Length);
        AudioClip selectedClip = clips[index];

        audioSource.PlayOneShot(selectedClip);

        // Eðer önleme aktifse, bu key için son çalma zamanýný güncelle
        if (preventReplay)
        {
            lastPlayedTimes[clipKey] = Time.time;
        }
    }
}
