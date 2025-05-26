using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Player_Sounder : MonoBehaviour
{
    [SerializeField] private Player_Character player;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] woodsAttack;
    [SerializeField] private AudioClip[] fletchersAttack;
    [SerializeField] private AudioClip[] fletchersReloads;
    [SerializeField] private AudioClip[] playerDeath;
    [SerializeField] private AudioClip[] playerHurt;
    [SerializeField] private AudioClip[] playerHeal;
    [SerializeField] private AudioClip[] playerSlide;
    [SerializeField] private AudioClip[] playerJump;
    [SerializeField] private AudioClip[] playerLand;
    [SerializeField] private AudioClip[] dynamiteIgnite;


    private void PlayFrom(AudioClip[] clips, GameObject source = null)
    {
        if (clips.Length != 0)
        {
            int index = Random.Range(0, clips.Length);
            audioSource.PlayOneShot(clips[index]);
        }
        else
        {
            Debug.LogWarning($"[SoundManager] Null AudioClip received from: {source?.name ?? "Unknown"}");
        }
    }


    public void PlayAttackSound()
    {
        if (player.character.characterName == "Woods")
        {
            //PlayFrom(woodsAttack);
        }
        if (player.character.characterName == "Fletcher")
        {
            PlayFrom(fletchersAttack);
        }
    }
    public void PlayFletchersReload()
    {
        PlayFrom(fletchersReloads);
    }
    public void PlayDeathSound()
    {
        PlayFrom(playerDeath);
    }
    public void PlayHurtSound()
    {
        PlayFrom(playerHurt);
    }
    public void PlayHealSound()
    {
        PlayFrom(playerHeal);
    }
    public void PlaySlideSound()
    {
        PlayFrom(playerSlide);
    }
    public void PlayIgniteSound()
    {
        PlayFrom(dynamiteIgnite);
    }
    public void PlayJumpSound()
    {
        //PlayFrom(playerJump);
    }
    public void PlayLandSound()
    {
        //PlayFrom(playerLand);
    }
}
