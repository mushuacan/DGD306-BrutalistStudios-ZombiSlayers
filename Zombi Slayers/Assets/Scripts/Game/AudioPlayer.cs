using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private AudioMixerGroup mixer;

    public void PlaySound()
    {
        PlaySoundDetached(clips[Random.Range(0, clips.Length)]);
    }
    private void PlaySoundDetached(AudioClip clip)
    {
        GameObject tempAudio = new GameObject("TempAudio");
        AudioSource aSource = tempAudio.AddComponent<AudioSource>();

        aSource.clip = clip;
        aSource.outputAudioMixerGroup = mixer;
        aSource.Play();

        // Sesin süresi kadar sonra objeyi yok et
        Destroy(tempAudio, clip.length);
    }

}
